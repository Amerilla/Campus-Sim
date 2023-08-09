using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public string nextScene;
        
        private GameManager _gameManager;
        private VisualElement _root;
        private VisualElement _logo;
        private UIDocument _uiDocument;
        private Button _start;
        private VisualElement _menuVisualElement;
        
        // Start is called before the first frame update
        void Awake()
        {
            
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.sortingOrder = 1;
            _root = _uiDocument.rootVisualElement;
            _menuVisualElement = _root.Q("Menu");
            _logo = _root.Q("Logo");
            _start = _menuVisualElement.Q<Button>("Start");
            _start.clicked += () => {
                Hide();
                SceneManager.LoadScene(nextScene);
            };
            _logo.Q<Button>("Quit").clicked += () => {
                Application.Quit();
                
            };

        }
        

        // Update is called once per frame
        void Update()
        {
        
        }
        private void Show(Scene scene, LoadSceneMode mode) {
            if (scene.name == "Menu") {
                _logo.visible = true;
                _menuVisualElement.visible = true;
                _menuVisualElement.SetEnabled(true);
                _uiDocument.sortingOrder = 1;
            }
        }
        
        private void Hide() {
            _logo.visible = false;
            _menuVisualElement.visible = false;
            _menuVisualElement.SetEnabled(false);
            _uiDocument.sortingOrder = -1;
        }
        
        private void OnEnable() {
            SceneManager.sceneLoaded += Show;
            
        }
    }
}
