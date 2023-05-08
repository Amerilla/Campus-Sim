using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Game;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoresHandler _scoresHandler;
    private ChoiceGenerator _choiceGen;
    private int _currentTurn;
    private Campus _campus;
    private UIBehaviour _UI;

    void Start() {
        var buildingsHandler = new BuildingsHandler(DeserializeList<BuildingStats>("HardData/Buildings.json"));
        _campus = new("EPFL-UNIL", 0, 0, 0, 0, 0, Campus.State.Neutral, buildingsHandler);
        
        _scoresHandler = new ScoresHandler(DeserializeList<MainScore>("HardData/ScoreCategories.json"));
        
        var choicesEconomie = DeserializeList<Choice>("HardData/Choices/Economie.json");
        _choiceGen = new ChoiceGenerator(choicesEconomie);
        var p = 0;
    }

    void Update() {
        
    }

    private List<T> DeserializeList<T>(string path) {
        string jsonPath = Path.Combine(Application.dataPath, path);
        using StreamReader r = new StreamReader(jsonPath);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<List<T>>(json);
    }

    private T DeserializeSingle<T>(string path) {
        string jsonPath = Path.Combine(Application.dataPath, path);
        using StreamReader r = new StreamReader(jsonPath);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    public void NextTurn() {



        _UI.UpdateMoney(_campus.GetBalance());
        _currentTurn++;
    }
}
