using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Data;
using Game;
using Newtonsoft.Json;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Action = Game.Action;

public class GameManager : MonoBehaviour
{

    public string _nextScene;
    private ScoresHandler _scoresHandler;
    private DataRecorder _recorder;
    private ChoiceGenerator _choiceGen;
    private int _currentTurn;
    private const int MaxScore = 100;
    private const int MaxTurn = 60;
    private Campus _campus;
    private ActionDetails _uiActionDetails;
    private HUD _uiHUD;
    private List<Action> _actionsToDo = new();
    private List<Consequence> _remainingConsequences = new();
    private List<SuccessBehaviour> _success;
    private Success _uiSuccess;

    public static GameManager Instance;
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (Instance != this) {
            Destroy(gameObject);
        }
        var buildingsHandler = new BuildingsHandler(DeserializeList<BuildingStats>("HardData/Buildings.json"));
        _campus = new("EPFL-UNIL", 0, 100100000, 0, 100000, 0, Campus.State.Neutral, buildingsHandler);
        _uiActionDetails = GameObject.Find("ActionDetails").GetComponent<ActionDetails>();
        _uiHUD = GameObject.Find("HUD").GetComponent<HUD>();
        _uiSuccess = GameObject.Find("Success").GetComponent<Success>();
        _recorder = new DataRecorder();
        

        _scoresHandler = new ScoresHandler(DeserializeList<Score>("HardData/Scores.json"));
        _success = DeserializeList<SuccessBehaviour>("HardData/Success.json");
        string root = "HardData/Choices";
        var choicesEco = DeserializeList<Choice>($"{root}/Economie.json");
        var choicesEnv = DeserializeList<Choice>($"{root}/Environnement.json");
        var choicesMob = DeserializeList<Choice>($"{root}/Mobilite.json");
        var choicesPop = DeserializeList<Choice>($"{root}/Population.json");
        var choicesCult = DeserializeList<Choice>($"{root}/Culture.json");
        var choicesEne = DeserializeList<Choice>($"{root}/Energie.json");
        var choicesAca = DeserializeList<Choice>($"{root}/Academique.json");
        Dictionary<ScoreType, List<Choice>> choices = new Dictionary<ScoreType, List<Choice>>() {
            { ScoreType.CULTURE, choicesCult },
            { ScoreType.ENERGIE, choicesEne },
            { ScoreType.ECONOMIE, choicesEco },
            { ScoreType.MOBILITE, choicesMob },
            { ScoreType.ACADEMIQUE, choicesAca },
            { ScoreType.POPULATION, choicesPop},
            { ScoreType.ENVIRONNEMENT, choicesEnv}
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

    void Start() {
        
    }


    public int GetMaxTurn() => MaxTurn;

    public int GetMaxScore() => MaxScore;

    private void LastTurn() {
        _uiHUD.Hide();
        _uiActionDetails.Disable();
        SceneManager.LoadScene(_nextScene);
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

    public Score GetScoreFromJSON(string name) => _scoresHandler.GetScore(name);
    
    public void NextTurn() {
        Record();
        _uiSuccess.Hide();
        UpdateRemainingConsequences();
        ExecuteActions();
        foreach (var success in _success) {
            _uiSuccess.Show(success.HasSuccess());
        }
        _scoresHandler.UpdateScores();
        _uiActionDetails.Hide();
        _uiHUD.ResetShowedScore();
        _uiHUD.UpdateHud(_scoresHandler.GetScore(ScoreType.ENVIRONNEMENT.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.POPULATION.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.ECONOMIE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.ENERGIE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.ACADEMIQUE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.CULTURE.ToString()).GetCurrentAndNextScore(),
            _scoresHandler.GetScore(ScoreType.MOBILITE.ToString()).GetCurrentAndNextScore(),_currentTurn);
        _currentTurn++;
        if (_currentTurn == MaxTurn) {
            LastTurn();
        }

    }

    private void Record() {
        HashSet<Score> scores = new() {
            new Score(_scoresHandler.GetScore(ScoreType.POPULATION.ToString())),
            new Score(_scoresHandler.GetScore(ScoreType.ENVIRONNEMENT.ToString())),
            new Score(_scoresHandler.GetScore(ScoreType.ECONOMIE.ToString())),
            new Score(_scoresHandler.GetScore(ScoreType.CULTURE.ToString())), 
            new Score(_scoresHandler.GetScore(ScoreType.ACADEMIQUE.ToString())),
            new Score(_scoresHandler.GetScore(ScoreType.ENERGIE.ToString())),
            new Score(_scoresHandler.GetScore(ScoreType.MOBILITE.ToString())),
        };
        HashSet<Action> actions = new HashSet<Action>(_actionsToDo);
        _recorder.RecordScores(scores, _currentTurn);
        _recorder.RecordActions(actions, _currentTurn);
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

    public DataRecorder GetRecorder() => _recorder;

}

