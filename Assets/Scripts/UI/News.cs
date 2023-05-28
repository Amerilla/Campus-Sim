using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI{
    public class News : MonoBehaviour
{
    private VisualElement _container;
    private Label _label;

    private void Awake() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _container = root.Q("Container");
        _label = _container.Q<Label>("News");
        Hide();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show() {
        _container.visible = true;
    }

    public void Hide() {
        _container.visible = false;
    }
}
}