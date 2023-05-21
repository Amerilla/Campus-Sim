using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        private GameManager _gameManager;
        private VisualElement _root;
        private UIDocument _uiDocument;
        private Button _start;
        private VisualElement _menuVisualElement;
        
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.sortingOrder = 1;
            _root = _uiDocument.rootVisualElement;
            _menuVisualElement = _root.Q("Menu");
            _start = _menuVisualElement.Q<Button>("Start");
            _start.clicked += () => {
                Hide();
                _gameManager.Intro();
            };

        }
        
        // Update is called once per frame
        void Update()
        {
        
        }
        public void Show() {
            _menuVisualElement.visible = true;
            _menuVisualElement.SetEnabled(true);
            _uiDocument.sortingOrder = 1;
        }
        
        private void Hide() {
            _menuVisualElement.visible = false;
            _menuVisualElement.SetEnabled(false);
            _uiDocument.sortingOrder = 0;
        }

    }
}
