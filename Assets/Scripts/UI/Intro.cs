using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class Intro : MonoBehaviour
    {
        private GameManager _gameManager;
        
        private VisualElement _letter;
        private VisualElement _root;
        private UIDocument _uiDocument;
        private Button _next;

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.sortingOrder = 0;
            _root = _uiDocument.rootVisualElement;
            _letter = _root.Q<VisualElement>("Letter");
            _letter.visible = false;
            _next = _letter.Q<Button>("Next");
            _next.clicked += () => {
                _gameManager.FirstTurn();
                Hide();
            };
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void Hide() {
            _uiDocument.sortingOrder = 0;
            _letter.visible = false;
            _letter.SetEnabled(false);
        }
        
        public void Show() {
            _uiDocument.sortingOrder = 1;
            _letter.visible = true;
            _letter.SetEnabled(true);
        }
    }
}
