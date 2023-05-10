using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Game;
using UnityEditor.UI;
using Action = System.Action;

public class UIBehaviour : MonoBehaviour
{
    private GameManager _gameManager;

    private ScoreType _shownCategory;
    private bool _shown;
    
    private VisualElement _root;
    
    private Label _moneyDisplay;
    private Label _turnDisplay;
    private Label _title;
    
    private Button _go;
    private Button _nextTurn;
    
    private Button _environmentButton;
    private ProgressBar _environmentBar;
    private ScrollView _environmentActions;

    private Button _energyButton;
    private ProgressBar _energyBar;
    private ScrollView _energyActions;
    
    private Button _academicButton;
    private ProgressBar _academicBar;
    private ScrollView _academicActions;

    private Button _mobilityButton;
    private ProgressBar _mobilityBar;
    private ScrollView _mobilityActions;

    private Button _populationButton;
    private ProgressBar _populationBar;
    private ScrollView _populationActions;

    private Button _economyButton;
    private ProgressBar _economyBar;
    private ScrollView _economyActions;

    private Button _cultureButton;
    private ProgressBar _cultureBar;
    private ScrollView _cultureActions;

    private Label _actionDescription;
    private Label _actionCost;
    private Label _actionDuration;
    
    

    private void Start() {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        Initialized();
    }

    private void Initialized() {
        _root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement topPane = _root.Q<VisualElement>("TopPane");
        VisualElement bottomPane = _root.Q<VisualElement>("BottomPane");
        VisualElement centerPane = _root.Q<VisualElement>("CenterPane");
        VisualElement leftPane = _root.Q<VisualElement>("LeftPane");

        bottomPane.style.backgroundColor = Color.clear;
        
        _actionDescription = bottomPane.Q<Label>("Description_ActionSelected");
        _actionDescription.text = "";
        _actionDescription.style.backgroundColor = Color.clear;
        _actionDescription.style.width = new StyleLength(500);
        _actionDescription.style.color = Color.white;
        _actionDescription.style.whiteSpace = WhiteSpace.Normal;
        _actionDuration = bottomPane.Q<Label>("Description_Duration");
        _actionDuration.text = "";
        _actionDuration.style.backgroundColor = Color.clear;
        _actionDuration.style.color = Color.white;
        _actionCost = bottomPane.Q<Label>("Description_Budget");
        _actionCost.text = "";
        _actionCost.style.backgroundColor = Color.clear;
        _actionCost.style.color = Color.white;
        
        _environmentActions = new ScrollView();
        _environmentActions.name = "Environment_Actions";
        _environmentActions.style.position = new StyleEnum<Position>(Position.Absolute);
        _environmentActions.style.left = new StyleLength(10);
        bottomPane.Add(_environmentActions);

        _economyActions = new ScrollView();
        _economyActions.name = "Economy_Actions";
        _economyActions.style.position = new StyleEnum<Position>(Position.Absolute);
        bottomPane.Add(_economyActions);
        
        _moneyDisplay = topPane.Q<VisualElement>("MoneyDisplay").Q<Label>("Amount");
        _turnDisplay = topPane.Q<VisualElement>("TimeDisplay").Q<Label>("Turn");
        _title = topPane.Q<Label>("Title");
        _title.text = "";
        
        _go = bottomPane.Q<VisualElement>("GoDisplay").Q<Button>("Go");
        _go.style.color = Color.white;
        
        _nextTurn = topPane.Q<Button>("Go");
        _nextTurn.style.color = Color.white;

        _environmentButton = leftPane.Q<Button>("Planet_Button");
        _environmentBar = centerPane.Q<ProgressBar>("ProgressBar_Environment");
        

        _energyButton = leftPane.Q<Button>("Energy_Button");
        _energyBar = centerPane.Q<ProgressBar>("ProgressBar_Energy");

        _academicButton = leftPane.Q<Button>("Academic_Button");
        _academicBar = centerPane.Q<ProgressBar>("ProgressBar_Academic");
        
        _mobilityButton = leftPane.Q<Button>("Mobility_Button");
        _mobilityBar = centerPane.Q<ProgressBar>("ProgressBar_Mobility");

        _economyButton = leftPane.Q<Button>("Economy_Button");
        _economyBar = centerPane.Q<ProgressBar>("ProgressBar_Economy");
        
        _populationButton = leftPane.Q<Button>("Pop_Button");
        _populationBar = centerPane.Q<ProgressBar>("ProgressBar_Population");
        
        _cultureButton = leftPane.Q<Button>("Culture_Button");
        _cultureBar = centerPane.Q<ProgressBar>("ProgressBar_Culture");
        
        
        
        
        _environmentButton.clicked += () => ShowScore(ScoreType.ENVIRONNEMENT);
        _energyButton.clicked += () => ShowScore(ScoreType.ENERGIE);
        _academicButton.clicked += () => ShowScore(ScoreType.ACADEMIQUE);
        _mobilityButton.clicked += () => ShowScore(ScoreType.MOBILITE);
        _economyButton.clicked += () => ShowScore(ScoreType.ECONOMIE);
        _populationButton.clicked += () => ShowScore(ScoreType.POPULATION);
        _cultureButton.clicked += () => ShowScore(ScoreType.CULTURE);

        _nextTurn.clicked += () => _gameManager.NextTurn();

        HideUI();
    }

    public void UpdateMoney(int money) {
        _moneyDisplay.text = $"{money} CHF";
    }

    public void UpdateTurn(int turn) {
        _turnDisplay.text = $"{turn} / 100";
    }

    public void UpdateProgressBars(List<float> scores) {

        _environmentBar.value = scores[0];
        _energyBar.value = scores[1];
        _academicBar.value = scores[2];
        _mobilityBar.value = scores[3];
        _economyBar.value = scores[4];
        _populationBar.value = scores[5];
        _cultureBar.value = scores[6];

    }

    private void ShowScore(ScoreType name) {
        if (name == _shownCategory && _shown) {
            HideUI();
            return;
        }
        HideAllActions();
        ShowScoreBar();
        _shownCategory = name;
        _shown = true;
        switch (name) {
            case ScoreType.ENVIRONNEMENT: 
                _title.text = "Environnement";
                _environmentActions.style.display = DisplayStyle.Flex;
                break;
            case ScoreType.ENERGIE:
                _title.text = "Energie";
                break;
            case ScoreType.ACADEMIQUE:
                _title.text = "Académique";
                break;
            case ScoreType.MOBILITE:
                _title.text = "Mobilité";
                break;
            case ScoreType.ECONOMIE:
                _title.text = "Economie";
                _economyActions.style.display = DisplayStyle.Flex;
                break;
            case ScoreType.POPULATION:
                _title.text = "Population";
                break;
            case ScoreType.CULTURE:
                _title.text = "Culture";
                break;
            default:
                break;
        }
        
    }

    public void CreateActions(List<Choice> choices, ScoreType score) {

        

        foreach (var choice in choices) {
            Game.Action posAction = choice.GetPositive();
            Game.Action negAction = choice.GetNegative();
            Game.Action randomAction = choice.GetRandom();
            

            Button posButton = new Button();
            posButton.name = posAction.GetName();
            posButton.text = posAction.GetName();
            posButton.clicked += () => ActionDetail(posAction);
            posButton.style.width = new StyleLength(250);

            Button negButton = new Button();
            negButton.name = negAction.GetName();
            negButton.text = negAction.GetName();
            negButton.clicked += () => ActionDetail(negAction);
            negButton.style.width = new StyleLength(250);


            Button randomButton = new Button();
            randomButton.name = randomAction.GetName();
            randomButton.text = randomAction.GetName();
            negButton.clicked += () => ActionDetail(randomAction);
            randomButton.style.width = new StyleLength(250);
        
            switch (score) {
                case ScoreType.ENVIRONNEMENT:
                    _environmentActions.contentContainer.Add(posButton);
                    _environmentActions.contentContainer.Add(negButton);
                    _environmentActions.contentContainer.Add(randomButton);
                    _environmentActions.style.width = new StyleLength(800);
                    _environmentActions.style.height = new StyleLength(300);
                    _environmentActions.contentContainer.style.flexDirection = FlexDirection.Row;
                    _environmentActions.contentContainer.style.flexWrap = Wrap.Wrap;
                    _environmentActions.style.marginTop = new StyleLength(80);
                    _environmentActions.style.marginLeft = new StyleLength(50);
                    
                    
                    break;
                case ScoreType.ECONOMIE:
                    _economyActions.contentContainer.Add(posButton);
                    _economyActions.contentContainer.Add(negButton);
                    _economyActions.contentContainer.Add(randomButton);
                    _economyActions.style.width = new StyleLength(800);
                    _economyActions.style.height = new StyleLength(300);
                    _economyActions.contentContainer.style.flexDirection = FlexDirection.Row;
                    _economyActions.contentContainer.style.flexWrap = Wrap.Wrap;
                    _economyActions.style.marginLeft = new StyleLength(50);
                    _economyActions.style.marginTop = new StyleLength(80);
                    
                    break;
                default:
                    break;

            }

        }
        
    }

    private void HideUI() {
        _shown = false;
        _title.text = "";
        HideAllActions();
        HideScore();
        HideDetails();
    }

    private void HideDetails() {
        _actionDescription.style.display = DisplayStyle.None;
        _actionDuration.style.display = DisplayStyle.None;
        _actionCost.style.display = DisplayStyle.None;
        _go.style.display = DisplayStyle.None;
    }

    private void HideAllActions() {
        _economyActions.style.display = DisplayStyle.None;
        _environmentActions.style.display = DisplayStyle.None;

    }

    private void HideScore() {
        _environmentBar.style.display = DisplayStyle.None;
        _energyBar.style.display = DisplayStyle.None;
        _economyBar.style.display = DisplayStyle.None;
        _academicBar.style.display = DisplayStyle.None;
        _cultureBar.style.display = DisplayStyle.None;
        _mobilityBar.style.display = DisplayStyle.None;
        _populationBar.style.display = DisplayStyle.None;

    }

    private void ShowScoreBar() {
        _environmentBar.style.display = DisplayStyle.Flex;
        _energyBar.style.display = DisplayStyle.Flex;
        _economyBar.style.display = DisplayStyle.Flex;
        _academicBar.style.display = DisplayStyle.Flex;
        _cultureBar.style.display = DisplayStyle.Flex;
        _mobilityBar.style.display = DisplayStyle.Flex;
        _populationBar.style.display = DisplayStyle.Flex;
        
    }

    private void ActionDetail(Game.Action action) {
        _actionDescription.text = action.GetDescription();
        _actionDescription.style.display = DisplayStyle.Flex;
        if (action.GetMoneyChange() > 0) {
            _actionCost.text = $"Gain de {action.GetMoneyChange()} CHF";
        }
        else {
            _actionCost.text = $"Perte de {action.GetMoneyChange()} CHF";

        }
        
        _actionCost.style.display = DisplayStyle.Flex;
        _actionDuration.text = $"Dure {action.GetDuration()} tours";
        _actionDuration.style.display = DisplayStyle.Flex;
        _go.style.display = DisplayStyle.Flex;
        _go.clicked += () => _gameManager.AddActionToDo(action);

    }
}
