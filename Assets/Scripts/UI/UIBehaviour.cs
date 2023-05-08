using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class UIBehaviour : MonoBehaviour
{
    private GameManager _gameManager;
    
    private Label _moneyDisplay;
    private Label _turnDisplay;
    private Label _title;
    
    private Button _go;
    
    private Button _environmentButton;
    private ProgressBar _environmentBar;

    private Button _energyButton;
    private ProgressBar _energyBar;
    
    private Button _academicButton;
    private ProgressBar _academicBar;

    private Button _mobilityButton;
    private ProgressBar _mobilityBar;

    private Button _populationButton;
    private ProgressBar _populationBar;

    private Button _economyButton;
    private ProgressBar _economyBar;

    private Button _cultureButton;
    private ProgressBar _cultureBar;
    
    

    private void Start() {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement topPane = root.Q<VisualElement>("TopPane");
        VisualElement bottomPane = root.Q<VisualElement>("BottomPane");
        VisualElement centerPane = root.Q<VisualElement>("CenterPane");
        
        
        _moneyDisplay = topPane.Q<VisualElement>("MoneyDisplay").Q<Label>("Amount");
        _turnDisplay = topPane.Q<VisualElement>("TimeDisplay").Q<Label>("Turn");
        _title = topPane.Q<Label>("Title");
        
        _go = bottomPane.Q<VisualElement>("GoDisplay").Q<Button>("Go");
        _environmentBar = centerPane.Q<ProgressBar>("ProgressBar_Environment");
        _energyBar = centerPane.Q<ProgressBar>("ProgressBar_Energy");
        _academicBar = centerPane.Q<ProgressBar>("ProgressBar_Academic");
        _mobilityBar = centerPane.Q<ProgressBar>("ProgressBar_Mobility");
        _economyBar = centerPane.Q<ProgressBar>("ProgressBar_Economy");
        _populationBar = centerPane.Q<ProgressBar>("ProgressBar_Population");
        _cultureBar = centerPane.Q<ProgressBar>("ProgressBar_Culture");
        
        
        _go.clicked += () => _gameManager.NextTurn();
        

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

    public void ShowScore(string name) {
        switch (name) {
            case "env" {
                
            }
        }
        
        
    }
    
}
