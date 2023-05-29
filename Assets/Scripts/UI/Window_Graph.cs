using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace UI
{

    public class Window_Graph : MonoBehaviour
    {
        [SerializeField] private Sprite _circleSprite;
        private RectTransform _graphContainer;

        [SerializeField] private int _width = 1920;
        [SerializeField] private int _height = 1080;

        public string nextScene;
        
        private int _maxXValue = 10;
        private int _maxYValue = 100;

        private GameObject _window;

        private Data.DataRecorder _recorder;

        public TextMeshProUGUI labelPrefab;
        
        private void Awake() {
            _graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
            _graphContainer.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
        }

        private void OnEnable() {
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        }

        private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1) {
            _recorder = GameManager.Instance.GetRecorder();
            _maxXValue = GameManager.Instance.GetMaxTurn();
            _maxYValue = GameManager.Instance.GetMaxScore();
            ScoresShowGraphs(_recorder.GetScores);
            
            
        }

        private void ScoresShowGraphs( Dictionary<int, HashSet<Game.Score>> scores) {
            List<Color> colors = new List<Color>() {
                new Color(0.96f, 0.39f, 0.09f),
                new Color(0.15f, 0.16f, 0.24f),
                new Color(0.11f, 0.60f, 0.55f),
                new Color(0.20f, 0.00f, 0.40f),
                new Color(0.60f, 0.00f, 0.00f),
                new Color(0.00f, 0.60f, 0.00f),
                new Color(0.00f, 0.00f, 0.60f)
            };
            int colorIndex = 0;
            foreach (Game.ScoreType scoreType in Enum.GetValues(typeof(Game.ScoreType))) {
                List<int> scoreValues = new List<int>();
                ColorLabel(scoreType.ToString(),colors[colorIndex % colors.Count],colorIndex);
                foreach (KeyValuePair<int,HashSet<Game.Score>> entry in scores) {
                    foreach (Game.Score score in entry.Value) {
                        if (score.GetScoreType() == scoreType) {
                            scoreValues.Add(score.GetValue());
                        }
                    }
                }

                // Cycle through the colors for each graph.
                Color color = colors[colorIndex % colors.Count];

                // Show the graph for this score type.
                ShowGraph(scoreValues, color);

                colorIndex++;
            }
            XAxis(_maxXValue);
            YAxis(_maxYValue);
            NextButton();
            
        }

        private GameObject CreateCircle(Vector2 anchoredposition) {
            GameObject gameObject = new GameObject("circle", typeof(Image));
            gameObject.transform.SetParent(_graphContainer,false);
            gameObject.GetComponent<Image>().sprite = _circleSprite;
            gameObject.GetComponent<Image>().color = Color.black;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredposition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            return gameObject;
        }

        private void ColorLabel(string name, Color color, int index) {
            TextMeshProUGUI label = Instantiate(labelPrefab);
            label.text = name;
            label.color = color;
            label.gameObject.transform.SetParent(_graphContainer,false);
            label.rectTransform.anchorMin = new Vector2(0, 0);
            label.rectTransform.anchorMax = new Vector2(0, 0);
            label.fontSize = 24;
            float xSize = (float)_width / _maxXValue;
            label.rectTransform.anchoredPosition = new Vector2((2 + 8*index) * xSize, _height + 20);
        }

        private void NextButton() {
            GameObject buttonGameObject = new GameObject("Button", typeof(Button));
            buttonGameObject.transform.SetParent(_graphContainer,false);
            
            RectTransform rectTransform = buttonGameObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(160, 50);
            rectTransform.anchoredPosition = new Vector2(1700, 900);
            Button buttonComponent = buttonGameObject.GetComponent<Button>();
            Image imageComponent = buttonGameObject.AddComponent<Image>();
            imageComponent.color = new Color(0.96f, 0.39f, 0.09f);
            // Add a Text child game object to display the button's label
            GameObject textGameObject = new GameObject("Text");
            textGameObject.transform.SetParent(buttonGameObject.transform, false);
            
            // Set up the Text component
            Text textComponent = textGameObject.AddComponent<Text>();
            textComponent.text = "Suivant";
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.color = Color.black;
            textComponent.fontSize = 24;
            textComponent.alignment = TextAnchor.MiddleCenter;

            RectTransform textRectTransform = textGameObject.GetComponent<RectTransform>();
            textRectTransform.anchorMin = new Vector2(0, 0);
            textRectTransform.anchorMax = new Vector2(1, 1);
            textRectTransform.sizeDelta = new Vector2(0, 0);

            // Set the onClick event
            buttonComponent.onClick.AddListener(onClickAction);
            
        }

        private void onClickAction() {
            SceneManager.LoadScene(nextScene);
        }
        
        private void XAxis(int nbValue) {
            float xSize = (float)_width/ _maxXValue;
            float lastPos = 0;
            for (int i = 0; i < nbValue; i++) {
                float xPosition = i * xSize;
                GameObject circleGameObject = CreateCircle(new Vector2(xPosition,0));
                CreateDotConnection(new Vector2(lastPos,0), new Vector2(xPosition,0),
                    Color.black);
                lastPos = xPosition;
            }
        }
        private void YAxis(int nbValue) {
            float ySize = (float)_height / _maxYValue;
            float lastPos = 0;
            for (int i = 0; i < nbValue; i++) {
                float yPosition = i * ySize;
                if (i%10 == 0) {
                    GameObject circleGameObject = CreateCircle(new Vector2(0,yPosition));
                }
                CreateDotConnection(new Vector2(0,lastPos), new Vector2(0,yPosition),
                    Color.black);
                lastPos = yPosition;
            }
        }

        private void ShowGraph(List<int> valueList, Color color) {
            float xSize = (float)_width/ _maxXValue;
            float ySize = (float)_height / _maxYValue;
            GameObject lastCircleGameObject = null;
            for (int i = 0; i < valueList.Count; i++) {
                float xPosition = i * xSize;
                float yPosition = valueList[i]* ySize;
                GameObject circleGameObject = CreateCircle(new Vector2(xPosition,yPosition));
                if (lastCircleGameObject != null) {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, 
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition, color);
                }
                lastCircleGameObject = circleGameObject;
            }
        }
        
        private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color) {
            GameObject gameObject = new GameObject("dotConnection", typeof(Image));
            gameObject.transform.SetParent(_graphContainer,false);
            gameObject.GetComponent<Image>().color = color;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 dir = (dotPositionB - dotPositionA).normalized;
            float distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0,0);
            rectTransform.anchorMax = new Vector2(0,0);
            rectTransform.sizeDelta = new Vector2(distance,3f);
            rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
            //calculate the angle
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rectTransform.localEulerAngles = new Vector3(0,0,angle);
            
        }
        
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}