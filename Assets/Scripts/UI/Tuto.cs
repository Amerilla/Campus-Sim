
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour
{
    public string _nextScene;
    
    private VisualElement _root;
    private UIDocument _uiDocument;
    private Button _next;
    private VisualElement _global;

    // Start is called before the first frame update
    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _uiDocument.sortingOrder = 0;
        _root = _uiDocument.rootVisualElement;
        _global = _root.Q<VisualElement>("Global");
        _next = _global.Q<Button>("Next");
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
        _global.visible = false;
        _global.SetEnabled(false);
    }
        
    private void Show() {
        _global = _root.Q<VisualElement>("Global");
        _global.visible = true;
        _global.SetEnabled(true);
    }
}

