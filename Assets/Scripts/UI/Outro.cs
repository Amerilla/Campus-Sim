using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class Outro : MonoBehaviour
    {
        private VisualElement _letter;
        private VisualElement _root;
        private UIDocument _uiDocument;
        private Button _next;
        private Menu _menu;

        // Start is called before the first frame update
        void Start()
        {
            _uiDocument = GetComponent<UIDocument>();
            _menu = GameObject.Find("Menu").GetComponent<Menu>();
            _root = _uiDocument.rootVisualElement;
            _letter = _root.Q<VisualElement>("Letter");
            _next = _letter.Q<Button>("Next");
           Hide();
        }

        public void Show() {
            _letter.visible = true;
            _uiDocument.sortingOrder = 1;
            _next.clicked += () => {
                Hide();
                _menu.Show();
            };
        }

        public void Hide() {
            _letter.visible = false;
            _uiDocument.sortingOrder = 0;
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
