using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Data;
using Game;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, BuildingBehaviour> _buildings = new();
    private Dictionary<string, GameObject> _gameObjects = new();

    // Start is called before the first frame update
    void Start() {
        foreach (string s in Buildings.names) {
            _gameObjects.Add(s,GameObject.Find(s));
            _buildings.Add(s,GameObject.Find(s).GetComponent<BuildingBehaviour>());
        }
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("--------------------------");
        foreach (var (s, buildingBehaviour) in _buildings) {
            Debug.Log(s);
            Debug.Log(buildingBehaviour._name);
        }
    }
}
