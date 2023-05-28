using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Window_Graph : MonoBehaviour
    {
        [SerializeField] private Sprite _circleSprite;
        private RectTransform _graphContainer;

        [SerializeField] private int _width = 1920;
        [SerializeField] private int _height = 1080;

        private int _maxXValue = 10;
        private int _maxYValue = 100;

        private GameObject _window;

        private void Awake() {
            _graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
            _graphContainer.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _height);
        }

        private void OnEnable() {
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        }

        private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1) {
            List<int> valueList = new List<int>() { 5, 98, 56, 30, 22, 17, 15, 13, 17, 55 };
            List<int> valueList2 = new List<int>() { 7, 48, 36, 20, 12, 27, 25, 23, 27, 45 };
            List<int> valueList3 = new List<int>() { 9, 38, 46, 10, 32, 37, 35, 33, 37, 35 };
            ShowGraph(valueList, Color.red);
            ShowGraph(valueList2, Color.green);
            ShowGraph(valueList3, Color.blue);
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

        private void ShowGraph(List<int> valueList, Color color) {
            float graphHeight = _graphContainer.sizeDelta.y;
            float yMaximum = _maxXValue;
            float xSize = _width / _maxXValue;
            GameObject lastCircleGameObject = null;
            for (int i = 0; i < valueList.Count; i++) {
                float xPosition = i * xSize;
                float yPosition = (valueList[i] / yMaximum) * graphHeight;
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