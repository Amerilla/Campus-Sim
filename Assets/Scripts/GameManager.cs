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
        _UI = GameObject.Find("Game Information").GetComponent<UIBehaviour>();
        _scoresHandler = new ScoresHandler(DeserializeList<MainScore>("HardData/ScoreCategories.json"));

        string root = "HardData/Choices";
        var choicesEco = DeserializeList<Choice>($"{root}/Economie.json");
        var choicesEnv = DeserializeList<Choice>($"{root}/Environnement.json");
        var choicesMob = DeserializeList<Choice>($"{root}/Mobilite.json");
        var choicesPop = DeserializeList<Choice>($"{root}/Population.json");
        var choicesCult = DeserializeList<Choice>($"{root}/Culture.json");
        var choicesEne = DeserializeList<Choice>($"{root}/Energie.json");
        var choicesAca = DeserializeList<Choice>($"{root}/Academique.json");
        Dictionary<MainCategory, List<Choice>> choices = new Dictionary<MainCategory, List<Choice>>() {
            { MainCategory.CULTURE, choicesCult },
            { MainCategory.ENERGIE, choicesEne },
            { MainCategory.ECONOMIE, choicesEco },
            { MainCategory.MOBILITE, choicesMob },
            { MainCategory.ACADEMIQUE, choicesAca }
        };
        _choiceGen = new ChoiceGenerator(choices);
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


        _UI.UpdateProgressBars(_scoresHandler.GetScores());
        _UI.UpdateMoney(_campus.GetBalance());
        _currentTurn++;
        _UI.UpdateTurn(_currentTurn);
    }
}
