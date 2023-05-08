using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Game;
using Action = System.Action;

public class UIBehaviour : MonoBehaviour
{
    private GameManager _gameManager;

    private VisualElement _root;
    
    private Label _moneyDisplay;
    private Label _turnDisplay;
    private Label _title;
    
    private Button _go;
    
    private Button _environmentButton;
    private ProgressBar _environmentBar;
    private ListView _environmentActions;

    private Button _energyButton;
    private ProgressBar _energyBar;
    private ListView _energyActions;
    
    private Button _academicButton;
    private ProgressBar _academicBar;
    private ListView _academicActions;

    private Button _mobilityButton;
    private ProgressBar _mobilityBar;
    private ListView _mobilityActions;

    private Button _populationButton;
    private ProgressBar _populationBar;
    private ListView _populationActions;

    private Button _economyButton;
    private ProgressBar _economyBar;
    private ListView _economyActions;

    private Button _cultureButton;
    private ProgressBar _cultureBar;
    private ListView _cultureActions;
    
    
    

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
        
        _environmentActions = new ListView();
        _environmentActions.name = "Environment_Actions";
        _environmentActions.style.position = new StyleEnum<Position>(Position.Relative);
        _environmentActions.style.left = 10;
        _environmentActions.style.top = 60;
        _environmentActions.style.display = DisplayStyle.None;

        _economyActions = new ListView();
        _economyActions.name = "Economy_Actions";
        _economyActions.style.position = new StyleEnum<Position>(Position.Absolute);
        _economyActions.style.left = 0;
        _economyActions.style.top = 0;
        _economyActions.style.display = DisplayStyle.Flex;
        _economyActions.style.backgroundColor = new StyleColor(Color.green);
        
        bottomPane.Add(_economyActions);

        _moneyDisplay = topPane.Q<VisualElement>("MoneyDisplay").Q<Label>("Amount");
        _turnDisplay = topPane.Q<VisualElement>("TimeDisplay").Q<Label>("Turn");
        _title = topPane.Q<Label>("Title");
        
        _go = bottomPane.Q<VisualElement>("GoDisplay").Q<Button>("Go");

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
        
        
        
        _go.clicked += () => _gameManager.NextTurn();
        _environmentButton.clicked += () => ShowScore(MainCategory.ENVIRONNEMENT);
        _energyButton.clicked += () => ShowScore(MainCategory.ENERGIE);
        _academicButton.clicked += () => ShowScore(MainCategory.ACADEMIQUE);
        _mobilityButton.clicked += () => ShowScore(MainCategory.MOBILITE);
        _economyButton.clicked += () => ShowScore(MainCategory.ECONOMIE);
        _populationButton.clicked += () => ShowScore(MainCategory.POPULATION);
        _cultureButton.clicked += () => ShowScore(MainCategory.CULTURE);


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

    private void ShowScore(MainCategory name) {
        switch (name) {
            case MainCategory.ENVIRONNEMENT: 
                _title.text = "Environnement";
                break;
            case MainCategory.ENERGIE:
                _title.text = "Energie";
                break;
            case MainCategory.ACADEMIQUE:
                _title.text = "Académique";
                break;
            case MainCategory.MOBILITE:
                _title.text = "Mobilité";
                break;
            case MainCategory.ECONOMIE:
                _title.text = "Economie";
                _economyActions.style.display = DisplayStyle.Flex;
                break;
            case MainCategory.POPULATION:
                _title.text = "Population";
                break;
            case MainCategory.CULTURE:
                _title.text = "Culture";
                break;
            default:
                break;
        }
        
    }

    public void CreateActions(List<Choice> choices, MainCategory score) {
        foreach (var choice in choices) {
            Game.Action posAction = choice.GetPositive();
            Game.Action negAction = choice.GetNegative();
            Game.Action randomAction = choice.GetRandom();

            Button posButton = new Button();
            posButton.name = posAction.GetName();
            posButton.text = posAction.GetName();
            posButton.style.height = new StyleLength(33);
            
            Button negButton = new Button();
            negButton.name = negAction.GetName();
            negButton.text = negAction.GetName();
            negButton.style.height = new StyleLength(33);

            Button randomButton = new Button();
            randomButton.name = randomAction.GetName();
            randomButton.text = randomAction.GetName();
            randomButton.style.height = new StyleLength(33);

            switch (score) {
                case MainCategory.ENVIRONNEMENT:
                    _environmentActions.bindItem = new Action<VisualElement, int>((element, i) => {
                        element.name = "Actions_Container";
                        element.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Column);
                        element.Add(posButton);
                        element.Add(negButton);
                        element.Add(randomButton);

                    });
                    break;
                case MainCategory.ECONOMIE:
                    _economyActions.bindItem = new Action<VisualElement, int>((element, i) => {
                        element.name = "Actions_Container";
                        element.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Column);
                        element.Add(posButton);
                        element.Add(negButton);
                        element.Add(randomButton);

                    });
                    break;
                default:
                    break;

            }
            
        }
    }
    
}
