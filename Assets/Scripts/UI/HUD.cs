using System;
using Game;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        private GameManager _gameManager;
    
        private VisualElement _root;
        private UIDocument _uiDocument;
        private Button _environmentGroupBox;
        private Button _populationGroupBox;
        private Button _economyGroupBox;
        private Button _energyGroupBox;
        private Button _academicGroupBox;
        private Button _cultureGroupBox;
        private Button _mobilityGroupBox;
        private VisualElement _toolBar;
        private Button _turnGroupBox;
        private ScoreType? _showedScoreType = null;
    
        // Start is called before the first frame update
        void Awake() {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            InitGroupBox();
            
        }

        private void InitGroupBox() {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            _environmentGroupBox = _root.Q("Toolbar").Q<VisualElement>("Scores").Q<Button>("Env");
            _populationGroupBox = _root.Q("Toolbar").Q<VisualElement>("Scores").Q<Button>("Pop");
            _economyGroupBox = _root.Q("Toolbar").Q<VisualElement>("Scores").Q<Button>("Eco");
            _energyGroupBox = _root.Q("Toolbar").Q<VisualElement>("Scores").Q<Button>("Ener");
            _academicGroupBox = _root.Q("Toolbar").Q<VisualElement>("Scores").Q<Button>("Aca");
            _cultureGroupBox = _root.Q("Toolbar").Q<VisualElement>("Scores").Q<Button>("Cult");
            _mobilityGroupBox = _root.Q("Toolbar").Q<VisualElement>("Scores").Q<Button>("Mob");
            _turnGroupBox = _root.Q("Toolbar").Q<Button>("Turn");
        }

        private void Start() {
           
        }

        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name == "GameView") {
                Show();
            } else {
                Hide();
            }
        }
        
        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void Show() {
            _root.Q("Toolbar").visible = true;
            _root.Q("Toolbar").SetEnabled(true);
            _uiDocument.sortingOrder = 1;
        }

        public void Hide() {
            _root.Q("Toolbar").visible = false;
            _uiDocument.sortingOrder = 0;
        }

        public void UpdateHud((int, int) env, (int, int) pop, (int, int) eco, (int, int) ener, (int, int) aca,
            (int, int) cult, (int, int) mob, int currentTurn) {
            ScoresUpdate(env,pop,eco,ener,aca,cult,mob);
            ScoreGroupBoxUpdate(currentTurn,currentTurn,_turnGroupBox);
        
        }

        public void InitHud((int, int) env, (int, int) pop, (int, int) eco, (int, int) ener, (int, int) aca,
            (int, int) cult, (int, int) mob,(int,int) turn, ActionDetails actionDetails) {
            _showedScoreType = null;
            ScoresInit(env,pop,eco,ener,aca,cult,mob,actionDetails);
            ScoreGroupBoxInit(turn.Item1,turn.Item2,_turnGroupBox,null,null);
            _turnGroupBox.clicked += () => _gameManager.NextTurn();
        }

        public void ScoresInit((int, int) env, (int, int) pop, (int, int) eco, (int, int) ener, (int, int) aca,
            (int, int) cult, (int, int) mob, ActionDetails actionDetails) {
            ScoreGroupBoxInit(env.Item1,env.Item2, _environmentGroupBox,ScoreType.ENVIRONNEMENT,actionDetails);
            ScoreGroupBoxInit(pop.Item1,pop.Item2,_populationGroupBox,ScoreType.POPULATION,actionDetails);
            ScoreGroupBoxInit(eco.Item1,eco.Item2, _economyGroupBox,ScoreType.ECONOMIE,actionDetails);
            ScoreGroupBoxInit(ener.Item1,ener.Item2,_energyGroupBox,ScoreType.ENERGIE,actionDetails);
            ScoreGroupBoxInit(aca.Item1,aca.Item2,_academicGroupBox,ScoreType.ACADEMIQUE,actionDetails);
            ScoreGroupBoxInit(cult.Item1,cult.Item2,_cultureGroupBox, ScoreType.CULTURE,actionDetails);
            ScoreGroupBoxInit(mob.Item1,mob.Item2,_mobilityGroupBox, ScoreType.MOBILITE,actionDetails);
        }

        public void ResetShowedScore() {
            if (_showedScoreType != null) {
                ScoreGroupBoxClearBorder(_environmentGroupBox);
                ScoreGroupBoxClearBorder(_populationGroupBox);
                ScoreGroupBoxClearBorder(_academicGroupBox);
                ScoreGroupBoxClearBorder(_economyGroupBox);
                ScoreGroupBoxClearBorder(_energyGroupBox);
                ScoreGroupBoxClearBorder(_cultureGroupBox);
                ScoreGroupBoxClearBorder(_mobilityGroupBox);
                _showedScoreType = null;
            }
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
    
        private void ScoreGroupBoxUpdate(int value, int nextValue, Button groupBox) {
            Label label = groupBox.Q("Score").Q<Label>("Value");
            ProgressBar pbCurrentFront = groupBox.Q("ProgressBar").Q<ProgressBar>("ProgressCurrentFront");
            var innerPbCFront = pbCurrentFront.Q(className: "unity-progress-bar__progress");
            ProgressBar pbCurrentBack = groupBox.Q("ProgressBar").Q<ProgressBar>("ProgressCurrentBack");
            var innerPbCBack = pbCurrentBack.Q(className: "unity-progress-bar__progress");
            Color red = new Color(0.95f, 0.24f, 0.24f);
            Color green = new Color(0.67f, 0.95f, 0.24f);
            Color color = new Color(0.58f, 0.72f, 0.82f);
            label.text = $"{value}";
            if (value > nextValue) {
                innerPbCFront.style.backgroundColor = color;
                innerPbCBack.style.backgroundColor = red;
                pbCurrentFront.value = value ;
                pbCurrentBack.value = nextValue;
            }
            else {
                innerPbCFront.style.backgroundColor = green;
                innerPbCBack.style.backgroundColor = color;
                pbCurrentFront.value = nextValue;
                pbCurrentBack.value = value;
            }
        }
    
        private void ScoreGroupBoxInit(int value, int maxValue, Button groupBox,ScoreType? scoreType,
                                        [CanBeNull] ActionDetails actionDetails) {
            if (scoreType != null && actionDetails != null) {
                groupBox.clicked += () => {
                    actionDetails.ShowActions((ScoreType)scoreType);
                    ScoreGroupBoxHighlight((ScoreType)scoreType);
                };
            }
            Label label = groupBox.Q("Score").Q<Label>("Value");
            ProgressBar pbCurrentFront = groupBox.Q("ProgressBar").Q<ProgressBar>("ProgressCurrentFront");
            var innerPbCFront = pbCurrentFront.Q(className: "unity-progress-bar__progress");
            var backgroundPbCFront = pbCurrentFront.Q(className: "unity-progress-bar__background");
            ProgressBar pbCurrentBack = groupBox.Q("ProgressBar").Q<ProgressBar>("ProgressCurrentBack");
            var innerPbCBack = pbCurrentBack.Q(className: "unity-progress-bar__progress");
            var backgroundPbCBack = pbCurrentBack.Q(className: "unity-progress-bar__background");
            

            Color color = new Color(0.58f, 0.72f, 0.82f);
            
            label.text = $"{value}";

            innerPbCFront.style.backgroundColor = color;
            innerPbCBack.style.backgroundColor = color;
            backgroundPbCFront.style.backgroundColor = Color.clear;
            backgroundPbCBack.style.backgroundColor = Color.clear;
            pbCurrentFront.value = value;
            pbCurrentFront.highValue = maxValue;
            pbCurrentBack.value = value;
            pbCurrentBack.highValue = maxValue;
        
        }

        private void ScoreGroupBoxHighlight(ScoreType scoreType) {
            ScoreGroupBoxClearBorder(_environmentGroupBox);
            ScoreGroupBoxClearBorder(_populationGroupBox);
            ScoreGroupBoxClearBorder(_academicGroupBox);
            ScoreGroupBoxClearBorder(_economyGroupBox);
            ScoreGroupBoxClearBorder(_energyGroupBox);
            ScoreGroupBoxClearBorder(_cultureGroupBox);
            ScoreGroupBoxClearBorder(_mobilityGroupBox);
            
            
            if (scoreType == _showedScoreType) {
                _showedScoreType = null;
                return;
            }

            _showedScoreType = scoreType;
            switch (scoreType) {
                case ScoreType.ENVIRONNEMENT:
                    ScoreGroupBoxShowBorder(_environmentGroupBox);
                    break;
                case ScoreType.POPULATION:
                    ScoreGroupBoxShowBorder(_populationGroupBox);
                    break;
                case ScoreType.ECONOMIE:
                    ScoreGroupBoxShowBorder(_economyGroupBox);
                    break;
                case ScoreType.ENERGIE :
                    ScoreGroupBoxShowBorder(_energyGroupBox);
                    break;
                case ScoreType.ACADEMIQUE:
                    ScoreGroupBoxShowBorder(_academicGroupBox);
                    break;
                case ScoreType.CULTURE:
                    ScoreGroupBoxShowBorder(_cultureGroupBox);
                    break;
                case ScoreType.MOBILITE:
                    ScoreGroupBoxShowBorder(_mobilityGroupBox);
                    break;
                    
            }
        }

        private void ScoreGroupBoxClearBorder(Button groupBox) {
            groupBox.Q("ProgressBar").style.borderBottomWidth = 0;
        }

        private void ScoreGroupBoxShowBorder(Button groupBox) {
            groupBox.Q("ProgressBar").style.borderBottomWidth = 10;
            groupBox.Q("ProgressBar").style.borderBottomRightRadius = 10;
            groupBox.Q("ProgressBar").style.borderBottomLeftRadius = 10;
        }
    
    
    }
}
