using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBehaviour : MonoBehaviour
{
    private GameManager _gameManager;
    private Label _moneyDisplay;
    private Label _turn;
    private Button _go;
    

    private void Start() {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement topPane = root.Q<VisualElement>("TopPane");
        VisualElement bottomPane = root.Q<VisualElement>("BottomPane");
        
        _moneyDisplay = topPane.Q<VisualElement>("MoneyDisplay").Q<Label>("Amount");
        _turnDisplay = topPane.Q<VisualElement>("TimeDisplay").Q<Label>("Turn");
        _go = bottomPane.Q<VisualElement>("GoDisplay").Q<Button>("Go");
        
        _go.clicked += () => _gameManager.NextTurn();

    }

    public void UpdateMoney(int money) {
        _moneyDisplay.text = $"{money} CHF";
    }

    public void UpdateTurn(int turn) {
        _turnDisplay.text = $"{turn} / 100";
    }
}
