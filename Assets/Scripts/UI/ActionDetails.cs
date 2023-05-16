using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionDetails : MonoBehaviour
{
    private VisualElement _root;
    
    private ScrollView _actions;
    private VisualElement _details;
    private List<Button> _environmentActions = new List<Button>();
    private List<Button> _populationActions = new List<Button>();
    private List<Button> _economyActions = new List<Button>();
    private List<Button> _energyActions = new List<Button>();
    private List<Button> _academicActions = new List<Button>();
    private List<Button> _cultureActions = new List<Button>();
    private List<Button> _mobilityActions = new List<Button>();




    // Start is called before the first frame update
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _actions = _root.Q<GroupBox>("Actions").Q<ScrollView>("Actions");
        _details = _root.Q<GroupBox>("Actions").Q<VisualElement>("Details");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Button CreateActionButton(Game.Action action) {
        Button button = new();
        button.name = action.GetName();
        button.text = action.GetName();
        button.clicked += () => ShowAction(action);
        button.style.width = new StyleLength(250);
        return button;
    }
    

    public void CreateActions(List<Choice> choices, ScoreType score) {
        List<Button> buttons = new();
        foreach (var choice in choices) {
            buttons.Add(CreateActionButton(choice.GetPositive()));
            buttons.Add(CreateActionButton(choice.GetNegative()));
            buttons.Add(CreateActionButton(choice.GetRandom()));


            switch (score) {
                case ScoreType.ENVIRONNEMENT:
                    _environmentActions = buttons;
                    break;
                case ScoreType.POPULATION:
                    _populationActions = buttons;
                    break;
                case ScoreType.ECONOMIE:
                    _economyActions = buttons;
                    break;
                case ScoreType.ENERGIE:
                    _energyActions = buttons;
                    break;
                case ScoreType.ACADEMIQUE:
                    _academicActions = buttons;
                    break;
                case ScoreType.CULTURE:
                    _cultureActions = buttons;
                    break;
                case ScoreType.MOBILITE:
                    _mobilityActions = buttons;
                    break;
            }
        }
    }

    public void ShowActions(ScoreType scoreType) {
        switch (scoreType) {
            case ScoreType.ENVIRONNEMENT:
                foreach (var action in _environmentActions) {
                    _actions.contentContainer.Add(action);
                }
                break;
            case ScoreType.POPULATION:
                foreach (var action in _populationActions) {
                    _actions.contentContainer.Add(action);
                }
                break;
            case ScoreType.ECONOMIE:
                foreach (var action in _economyActions) {
                    _actions.contentContainer.Add(action);
                }
                break;
            case ScoreType.ENERGIE:
                foreach (var action in _energyActions) {
                    _actions.contentContainer.Add(action);
                }
                break;
            case ScoreType.ACADEMIQUE:
                foreach (var action in _academicActions) {
                    _actions.contentContainer.Add(action);
                }
                break;
            case ScoreType.CULTURE:
                foreach (var action in _cultureActions) {
                    _actions.contentContainer.Add(action);
                }
                break;
            case ScoreType.MOBILITE:
                foreach (var action in _mobilityActions) {
                    _actions.contentContainer.Add(action);
                }
                break;
        }
    }
    
    private void ShowAction(Game.Action action) {
        
    }
    
}
