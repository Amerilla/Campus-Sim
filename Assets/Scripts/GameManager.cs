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
    private int _currentTurn;
    private Campus _campus;
    // Start is called before the first frame update
    void Start() {
        
        foreach (string s in Buildings.names) {
            _gameObjects.Add(s,GameObject.Find(s));
            _buildings.Add(s,GameObject.Find(s).GetComponent<BuildingBehaviour>());
        }
        _campus = new("EPFL-UNIL", 0, 0, 0, 0, 0,
            Campus.State.Neutral);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void NextTurn() {




        _currentTurn++;
    }
}
