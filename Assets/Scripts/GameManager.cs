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

    void Start() {
        var buildingsHandler = new BuildingsHandler(Deserialize<BuildingStats>("HardData/Buildings.json"));
        _campus = new("EPFL-UNIL", 0, 0, 0, 0, 0, Campus.State.Neutral, buildingsHandler);
        
        _scoresHandler = new ScoresHandler(Deserialize<MainScore>("HardData/ScoreCategories.json"));
        
        var choicesEconomie = Deserialize<Choice>("HardData/Choices/Economie.json");
        _choiceGen = new ChoiceGenerator(choicesEconomie);
        var p = 0;
    }

    void Update() {
        
    }

    private List<BuildingStats> DeserializeBuildings() {
        string jsonPath = Path.Combine(Application.dataPath, "HardData/Buildings.json");
        using StreamReader r = new StreamReader(jsonPath);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<List<BuildingStats>>(json);
    }

    private List<MainScore> DeserializeScores() {
        string jsonPath = Path.Combine(Application.dataPath, "HardData/MainScores.json");
        using StreamReader r = new StreamReader(jsonPath);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<List<MainScore>>(json);
    }

    private List<Action> DeserializeActions() {
        string jsonPath = Path.Combine(Application.dataPath, "HardData/Choices/MainScores.json");
        using StreamReader r = new StreamReader(jsonPath);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<List<Action>>(json);
    }

    private List<T> Deserialize<T>(string path) {
        string jsonPath = Path.Combine(Application.dataPath, path);
        using StreamReader r = new StreamReader(jsonPath);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
    
    public void NextTurn() {




        _currentTurn++;
    }
}
