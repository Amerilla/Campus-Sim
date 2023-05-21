using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Game;
using Newtonsoft.Json;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoresHandler _scoresHandler;
    private ChoiceGenerator _choiceGen;
    private int _currentTurn;
    private const int MaxScore = 100;
    private const int MaxTurn = 100;
    private Campus _campus;
    private ActionDetails _uiActionDetails;
    private HUD _uiHUD;
    private Intro _uiIntro;
    private Outro _uiOutro;
    private List<Action> _actionsToDo = new();
    private List<Consequence> _remainingConsequences = new();

    void Start() {
        var buildingsHandler = new BuildingsHandler(DeserializeList<BuildingStats>("HardData/Buildings.json"));
        _campus = new("EPFL-UNIL", 0, 100100000, 0, 100000, 0, Campus.State.Neutral, buildingsHandler);
        _uiActionDetails = GameObject.Find("ActionDetails").GetComponent<ActionDetails>();
        _uiHUD = GameObject.Find("HUD").GetComponent<HUD>();
        _uiIntro = GameObject.Find("Letter-Intro").GetComponent<Intro>();
        _uiOutro = GameObject.Find("Letter-Outro").GetComponent<Outro>();
        
        _scoresHandler = new ScoresHandler(DeserializeList<Score>("HardData/Scores.json"));

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

        foreach (var choice in choices) {
            _uiActionDetails.CreateActions(choice.Value,choice.Key);
        }
        _currentTurn = 1;
        _uiHUD.InitHud(
        (_scoresHandler.GetScore(ScoreType.ENVIRONNEMENT.ToString()).GetValue(), MaxScore),
        (_scoresHandler.GetScore(ScoreType.POPULATION.ToString()).GetValue(), MaxScore),
        (_scoresHandler.GetScore(ScoreType.ECONOMIE.ToString()).GetValue(), MaxScore),
        (_scoresHandler.GetScore(ScoreType.ENERGIE.ToString()).GetValue(), MaxScore),
        (_scoresHandler.GetScore(ScoreType.ACADEMIQUE.ToString()).GetValue(), MaxScore),
        (_scoresHandler.GetScore(ScoreType.CULTURE.ToString()).GetValue(), MaxScore),
        (_scoresHandler.GetScore(ScoreType.MOBILITE.ToString()).GetValue(), MaxScore),
        (_currentTurn, MaxTurn),_uiActionDetails);
        _uiActionDetails.InitDetails();
        
    }

    private void NewGame() {
        /// New Game
    }
    

    void Update() {
        
    }

    public void Intro() {
        NewGame();
        _uiIntro.Show();
    }

    public void FirstTurn() {
        _uiHUD.Show();
        _uiActionDetails.Show();
    }

    private void LastTurn() {
        _uiHUD.Hide();
        _uiActionDetails.Disable();
        _uiOutro.Show();
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
        UpdateRemainingConsequences();
        ExecuteActions();
        _scoresHandler.UpdateScores();
        _uiActionDetails.Hide();
        _uiHUD.UpdateHud(_scoresHandler.GetScore(ScoreType.ENVIRONNEMENT.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.POPULATION.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.ECONOMIE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.ENERGIE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.ACADEMIQUE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.CULTURE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.MOBILITE.ToString()).GetCurrentAndNextScore(),_currentTurn);
        _currentTurn++;
        _actionsToDo.Clear();
        if (_currentTurn == MaxTurn) {
            LastTurn();
        }
        
        
    }

    private void ExecuteActions() {
        foreach (Action action in _actionsToDo) {
            AddRemainingConsequences(action.Execute(_currentTurn));
        }

        _actionsToDo.Clear();
    }
    
    public void AddActionToDo(Action action) {
        if (action.CanBeExecuted(_currentTurn)) {
            _actionsToDo.Add(action);
            action.SetWaiting(true);
            return;
        }
        action.SetWaiting(false);
    }
    
    private void AddRemainingConsequences(List<Consequence> consequences) {
        _remainingConsequences.AddRange(consequences);
    }

    private void UpdateRemainingConsequences() {
        List<Consequence> toRemove = new List<Consequence>();
        foreach (var consequence in _remainingConsequences) {
            if (consequence.Update()) {
                toRemove.Add(consequence);
            }
        }
        foreach (var consequence in toRemove) {
            _remainingConsequences.Remove(consequence);
        }
    }

    public int GetCurrentTurn() => _currentTurn;

}

