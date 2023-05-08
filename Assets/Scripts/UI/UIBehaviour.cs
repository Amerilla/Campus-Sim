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
        VisualElement topPane = root.Q<VisualElement>("TopPane").Q<VisualElement>("MoneyDisplay");
        VisualElement money = topPane.
        _moneyDisplay = money.Q<Label>("Amount");
        _turn = root.Q<Label>("Turn");
        _go = root.Q<Button>("Go");
        
        _go.clicked += () => _gameManager.NextTurn();

    }

    public void UpdateMoney(int money) {
        _moneyDisplay.text = $"{money}";
    }
    
    
    
    
    
}
