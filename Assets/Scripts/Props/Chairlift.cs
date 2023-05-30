using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace Props
{
    public class Chairlift : MonoBehaviour
    {
        public GameObject _chairPrefab;
        
        private const float _spawnInterval = 6f;
        
        private Queue<Vector3> _initialCheckpoints;
        private HashSet<Chair> _chairs;
        private bool _isSpawning = true;
        private int _invokeHandle;
        private Vector3 _spawnPoint;
        private bool _isEnabled;

        private void Awake() {
            _isEnabled = false;
            _initialCheckpoints = new();
            GameObject station1 = GameObject.Find("Station1");
            for (int j = 1; j <= 10; j++) {
                GameObject checkpoint = GameObject.Find($"Station1/Checkpoint{j}");
                _initialCheckpoints.Enqueue(checkpoint.transform.position);
            }

            for (int i = 1; i <= 5; i++) {
                for (int j = 1; j <= 2; j++) {
                    GameObject checkpoint = GameObject.Find($"Beam{i}/Checkpoint{j}");
                    _initialCheckpoints.Enqueue(checkpoint.transform.position);
                }
            }
            
            
            GameObject station2 = GameObject.Find("Station2");
            for (int j = 1; j <= 10; j++) {
                GameObject checkpoint = GameObject.Find($"Station2/Checkpoint{j}");
                _initialCheckpoints.Enqueue(checkpoint.transform.position);
            }
            
            for (int i = 5; i >= 1; i--) {
                for (int j = 3; j <= 4; j++) {
                    GameObject checkpoint = GameObject.Find($"Beam{i}/Checkpoint{j}");
                    _initialCheckpoints.Enqueue(checkpoint.transform.position);
                }
            }

            _chairs = new HashSet<Chair>();
            
        }

        public void Enable() {
            _isEnabled = true;
            GameObject chair = Instantiate(_chairPrefab, _initialCheckpoints.Peek(), Quaternion.identity, transform);
            _spawnPoint = _initialCheckpoints.Peek();
            _spawnPoint = new Vector3(_spawnPoint.x, _spawnPoint.y, _spawnPoint.z);
            chair.transform.rotation = _chairPrefab.transform.rotation;
            _chairs.Add(new Chair(chair, _initialCheckpoints));
            
            InvokeRepeating(nameof(SpawnChair), _spawnInterval, _spawnInterval);
        }

        private void SpawnChair() {
            GameObject chair = Instantiate(_chairPrefab, _spawnPoint, Quaternion.identity, transform);
            chair.transform.rotation = _chairPrefab.transform.rotation;
            _chairs.Add(new Chair(chair, _initialCheckpoints));
        }
        
        private void Update() {
            if (!_isEnabled) return;
            
            foreach (var chair in _chairs) {
                chair.Update();
            }

            if (_isSpawning && _chairs.Count > 1 &&
                (_chairs.ToList()[0].GetPosition() - _spawnPoint).magnitude < 10) {
                _isSpawning = false;
                CancelInvoke();
            }
        }
    }

    public class Chair
    {
        private const double THRESHOLD = 0.5;
        //private const double SPEED = 10;
        private const double SPEED = 20;
        private GameObject _gameObject;
        private Queue<Vector3> _checkPoints;

        public Chair(GameObject gameObject, IEnumerable<Vector3> initialCheckpoints) {
            _gameObject = gameObject;
            _checkPoints = new Queue<Vector3>(initialCheckpoints);
        }

        public Vector3 GetPosition() => _gameObject.transform.position;
        
        public void Update() {
            MoveTowardsTarget(_checkPoints.Peek());
            RotateTowardsTarget(_checkPoints.Peek());
        }
        
        private void MoveTowardsTarget(Vector3 target) {
            Vector3 direction = target - _gameObject.transform.position;
            if (direction.magnitude > THRESHOLD) {
                direction.Normalize();
                _gameObject.transform.position += direction * ((float)SPEED * Time.deltaTime);
            }

            if ((target - _gameObject.transform.position).magnitude < THRESHOLD) {
                _checkPoints.Enqueue(_checkPoints.Dequeue());
            }
        }
        
        void RotateTowardsTarget(Vector3 target) {
            Vector3 direction = target - _gameObject.transform.position;
            direction.y = 0f; // Ignore the y-component to only rotate around the y-axis

            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _gameObject.transform.rotation = targetRotation;
            }
        }
    }
}