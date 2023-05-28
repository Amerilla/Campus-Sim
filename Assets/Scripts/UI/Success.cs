using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class Success : MonoBehaviour
    {
        private VisualElement _container;
        private Label _name;
        private Label _description;
        private bool _showed = false;

        private void Awake() {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _container = root.Q("Container");
            _name = _container.Q("Container").Q<Label>("Name");
            _description = _container.Q("Container").Q<Label>("Description");
            Hide();

        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
        
        public void Show((string,string) valueTuple) {
            if (valueTuple.Item1 != null && valueTuple.Item2 != null) {
                _container.visible = true;
                _name.text = valueTuple.Item1;
                _description.text = valueTuple.Item2;
                _showed = true;
            }

        }

        public void Hide() {
            _container.visible = false;
            _name.text = "";
            _description.text = "";
        }
        
    }
}