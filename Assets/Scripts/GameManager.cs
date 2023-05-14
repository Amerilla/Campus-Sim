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
    private List<Action> _actionsToDo;

    void Start() {
        var buildingsHandler = new BuildingsHandler(DeserializeList<BuildingStats>("HardData/Buildings.json"));
        _campus = new("EPFL-UNIL", 0, 100100000, 0, 100000, 0, Campus.State.Neutral, buildingsHandler);
        //_UI = GameObject.Find("Game Information").GetComponent<UIBehaviour>();
        _scoresHandler = new ScoresHandler(DeserializeList<Score>("HardData/Scores.json"));
        _actionsToDo = new List<Action>();

        string root = "HardData/Choices";
        //var choicesEco = DeserializeList<Choice>($"{root}/Economie.json");
        //var choicesEnv = DeserializeList<Choice>($"{root}/Environnement.json");
        //var choicesMob = DeserializeList<Choice>($"{root}/Mobilite.json");
        //var choicesPop = DeserializeList<Choice>($"{root}/Population.json");
        //var choicesCult = DeserializeList<Choice>($"{root}/Culture.json");
        //var choicesEne = DeserializeList<Choice>($"{root}/Energie.json");
        var choicesAca = DeserializeList<Choice>($"{root}/Academique.json");
        Dictionary<ScoreType, List<Choice>> choices = new Dictionary<ScoreType, List<Choice>>() {
            //{ ScoreType.CULTURE, choicesCult },
            //{ ScoreType.ENERGIE, choicesEne },
            //{ ScoreType.ECONOMIE, choicesEco },
            //{ ScoreType.MOBILITE, choicesMob },
            { ScoreType.ACADEMIQUE, choicesAca },
            //{ ScoreType.POPULATION, choicesPop},
            //{ ScoreType.ENVIRONNEMENT, choicesEnv}
        };
        _choiceGen = new ChoiceGenerator(choices);
        var p = 0;
        //_UI.CreateActions(choicesEco,ScoreType.ECONOMIE);
        //_UI.CreateActions(choicesEnv,ScoreType.ENVIRONNEMENT);
        //_UI.CreateActions(choicesAca, ScoreType.ACADEMIQUE);
        
        NextTurn();
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

    public Score GetScore(string name) => _scoresHandler.GetScore(name);
    
    public void NextTurn() {
        ExecuteActions();
        //_scoresHandler.UpdateScores();  
        _campus.UpdateBalance();
        
        
        //_UI.UpdateProgressBars(_scoresHandler.GetScores());
        //_UI.UpdateMoney(_campus.GetBalance());
        _currentTurn++;
        //_UI.UpdateTurn(_currentTurn);
        _actionsToDo.Clear();   
    }

    private void ExecuteActions() {
        foreach (Action action in _actionsToDo) {
            action.Execute(_currentTurn,_campus);
        }
        _actionsToDo.Clear();
    }
    
    public void AddActionToDo(Action action) {
        _actionsToDo.Add(action);
    }
}

