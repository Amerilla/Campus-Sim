using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class ActionDetails : MonoBehaviour
    {
        private GameManager _gameManager;
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

        private ScoreType? _showedScoreType = null;




        // Start is called before the first frame update
        void Start() {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            _root = GetComponent<UIDocument>().rootVisualElement;
            _actions = _root.Q<GroupBox>("Actions").Q<ScrollView>("Actions");
            _details = _root.Q<GroupBox>("Actions").Q<VisualElement>("Details");

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void InitDetails() {
            Hide();
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

        public void ShowActions(ScoreType? scoreType) {
            _details.visible = false;
            if (_showedScoreType == scoreType) {
                _actions.visible = false;
                _showedScoreType = null;
                return;
            }
            _showedScoreType = scoreType;
            _actions.visible = true;
            _actions.contentContainer.Clear();
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
            _details.visible = true;
            Label description = _details.Q<Label>("Description");
            description.text = action.GetDescription();
            Button validation = _details.Q<Button>("Validation");
            if (action.IsWaiting()) {
                validation.clicked += null;
                validation.SetEnabled(false);
                validation.text = "Déjà selectionnée !";
                Color color = new Color(0.95f, 0.67f,0.24f);
                validation.style.backgroundColor = color;
            }
            else if (action.CanBeExecuted(_gameManager.GetCurrentTurn())) {
                validation.clicked += () => {
                    _gameManager.AddActionToDo(action);
                    _details.visible = false;
                };
                Color color = new Color(0.67f, 0.95f, 0.24f);
                validation.style.backgroundColor = color;
                validation.text = "C'est parti !";
                validation.SetEnabled(true);
            }
            else {
                validation.clicked += null;
                Color color = new Color(0.95f, 0.24f, 0.24f);
                validation.style.backgroundColor = new StyleColor(color);
                validation.text = " Pas Dispo !";
                validation.SetEnabled(false);
            }
            RequirementScores(_details.Q<GroupBox>("Needed"),action);
            GainsScores(_details.Q<GroupBox>("Gain"),action);
            

        }

        private void RequirementScores(GroupBox groupBox, Action action) {
            HideLabels(groupBox);
            List<Requirement> requirements = action.GetRequirements();
            foreach (var requirement in requirements) {
                switch (requirement.GetRequirementScoreType()) {
                    case ScoreType.CULTURE:
                        LabelUpdate(groupBox.Q<GroupBox>("Cult"), requirement.GetRequirement());
                        break;
                    case ScoreType.ECONOMIE:
                        LabelUpdate(groupBox.Q<GroupBox>("Eco"), requirement.GetRequirement());
                        break;
                    case ScoreType.ENERGIE:
                        LabelUpdate(groupBox.Q<GroupBox>("Ener"), requirement.GetRequirement());
                        break;
                    case ScoreType.ENVIRONNEMENT:
                        LabelUpdate(groupBox.Q<GroupBox>("Env"), requirement.GetRequirement());
                        break;
                    case ScoreType.MOBILITE:
                        LabelUpdate(groupBox.Q<GroupBox>("Mob"), requirement.GetRequirement());
                        break;
                    case ScoreType.ACADEMIQUE:
                        LabelUpdate(groupBox.Q<GroupBox>("Aca"), requirement.GetRequirement());
                        break;
                    case ScoreType.POPULATION:
                        LabelUpdate(groupBox.Q<GroupBox>("Pop"), requirement.GetRequirement());
                        break;
                    
                }
            }
            
        }
        

        private void GainsScores(GroupBox groupBox, Action action) {
            HideLabels(groupBox);
            List<Consequence> gains = action.GetConsequences();
            foreach (var gain in gains) {
                switch (gain.GetConsequenceScoreType()) {
                    case ScoreType.CULTURE:
                        LabelUpdate(groupBox.Q<GroupBox>("Cult"), gain.GetValue());
                        break;
                    case ScoreType.ECONOMIE:
                        LabelUpdate(groupBox.Q<GroupBox>("Eco"), gain.GetValue());
                        break;
                    case ScoreType.ENERGIE:
                        LabelUpdate(groupBox.Q<GroupBox>("Ener"), gain.GetValue());
                        break;
                    case ScoreType.ENVIRONNEMENT:
                        LabelUpdate(groupBox.Q<GroupBox>("Env"), gain.GetValue());
                        break;
                    case ScoreType.MOBILITE:
                        LabelUpdate(groupBox.Q<GroupBox>("Mob"), gain.GetValue());
                        break;
                    case ScoreType.ACADEMIQUE:
                        LabelUpdate(groupBox.Q<GroupBox>("Aca"), gain.GetValue());
                        break;
                    case ScoreType.POPULATION:
                        LabelUpdate(groupBox.Q<GroupBox>("Pop"), gain.GetValue());
                        break;
                                
                }
            }
        }
        
        private void LabelUpdate(GroupBox groupBox,int value) {
            Label label = groupBox.Q<Label>("Value");
            if (value == 0) {
                groupBox.SetEnabled(false);
                label.text = "-";
                return;
            }
            groupBox.SetEnabled(true);
            label.text = $"{value}";

        }

        private void HideLabels(GroupBox groupBox) {
            LabelUpdate(groupBox.Q<GroupBox>("Env"),0);
            LabelUpdate(groupBox.Q<GroupBox>("Pop"),0);
            LabelUpdate(groupBox.Q<GroupBox>("Ener"),0);
            LabelUpdate(groupBox.Q<GroupBox>("Eco"),0);
            LabelUpdate(groupBox.Q<GroupBox>("Aca"),0);
            LabelUpdate(groupBox.Q<GroupBox>("Cult"),0);
            LabelUpdate(groupBox.Q<GroupBox>("Mob"),0);
        }

        public void Hide() {
            _actions.visible = false;
            _showedScoreType = null;
            _details.visible = false;
            HideLabels(_details.Q<GroupBox>("Needed"));
            HideLabels(_details.Q<GroupBox>("Gain"));
        }
    }
}
