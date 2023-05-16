using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Game;
using UnityEditor.UI;
using Action = System.Action;
public class HUD : MonoBehaviour
{
    private VisualElement _root;
    private GroupBox _environmentGroupBox;
    private GroupBox _populationGroupBox;
    private GroupBox _economyGroupBox;
    private GroupBox _energyGroupBox;
    private GroupBox _academicGroupBox;
    private GroupBox _cultureGroupBox;
    private GroupBox _mobilityGroupBox;

    private GroupBox _turnGroupBox;
    
    // Start is called before the first frame update
    void Start() {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _environmentGroupBox = _root.Q<GroupBox>("Env");
        _populationGroupBox = _root.Q<GroupBox>("Pop");
        _economyGroupBox = _root.Q<GroupBox>("Eco");
        _energyGroupBox = _root.Q<GroupBox>("Ener");
        _academicGroupBox = _root.Q<GroupBox>("Aca");
        _cultureGroupBox = _root.Q<GroupBox>("Cult");
        _mobilityGroupBox = _root.Q<GroupBox>("Mob");

        _turnGroupBox = _root.Q<GroupBox>("Turn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHud((int, int) env, (int, int) pop, (int, int) eco, (int, int) ener, (int, int) aca,
        (int, int) cult, (int, int) mob, int currentTurn) {
        ScoresUpdate(env,pop,eco,ener,aca,cult,mob);
        TurnUpdate(currentTurn);
    }
    
    private void ScoresUpdate((int, int) env, (int, int) pop, (int, int) eco, (int, int) ener, (int, int) aca,
        (int, int) cult, (int, int) mob) {
        ScoreGroupBoxUpdate(env.Item1,env.Item2, _environmentGroupBox);
        ScoreGroupBoxUpdate(pop.Item1,pop.Item2,_populationGroupBox);
        ScoreGroupBoxUpdate(eco.Item1,eco.Item2, _economyGroupBox);
        ScoreGroupBoxUpdate(ener.Item1,ener.Item2,_energyGroupBox);
        ScoreGroupBoxUpdate(aca.Item1,aca.Item2,_academicGroupBox);
        ScoreGroupBoxUpdate(cult.Item1,cult.Item2,_cultureGroupBox);
        ScoreGroupBoxUpdate(mob.Item1,mob.Item2,_mobilityGroupBox);
    }
    
    private void ScoreGroupBoxUpdate(int value, int nextValue, GroupBox groupBox) {
        Label label = groupBox.Q<Label>("Value");
        ProgressBar pbCurrentFront = groupBox.Q<ProgressBar>("ProgressbarCurrentFront");
        ProgressBar pbCurrentBack = groupBox.Q<ProgressBar>("ProgressbarCurrentBack");

        label.text = $"{value}";
        if (value > nextValue) {
            pbCurrentBack.style.color = Color.blue;
            pbCurrentFront.style.backgroundColor = Color.clear;
            pbCurrentFront.style.color = Color.red;
            pbCurrentFront.value = nextValue;
            pbCurrentBack.value = value;
        }
        else {
            pbCurrentBack.style.color = Color.green;
            pbCurrentFront.style.backgroundColor = Color.clear;
            pbCurrentFront.style.color = Color.blue;
            pbCurrentFront.value = value;
            pbCurrentBack.value = nextValue;
        }
    }

    private void TurnUpdate(int currentTurn) {
        ScoreGroupBoxUpdate(currentTurn,currentTurn,_turnGroupBox);
    }
    
}
