using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


namespace UI
{
    public class Intro : MonoBehaviour
    {
        public string _nextScene;
        
        private VisualElement _letter;
        private VisualElement _root;
        private UIDocument _uiDocument;
        private Button _next;

        // Start is called before the first frame update
        void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.sortingOrder = 0;
            _root = _uiDocument.rootVisualElement;
            _letter = _root.Q<VisualElement>("Letter");
            _next = _letter.Q<Button>("Next");
            _next.clicked += () => {
                SceneManager.LoadScene(_nextScene);
                Hide();
            };
        }

        
        private void OnEnable() {
            SceneManager.sceneLoaded += (arg0, mode) => Show();
        }
        


        private void Hide() {
            _uiDocument.sortingOrder = 0;
            _letter.visible = false;
            _letter.SetEnabled(false);
        }
        
        private void Show() {
            _letter = _root.Q<VisualElement>("Letter");
            _letter.visible = true;
            _letter.SetEnabled(true);
        }
    }
}
