using System;
using UnityEngine;

namespace Props
{
    public class PropManager : MonoBehaviour
    {
        public GameObject chairLift;
        public GameObject vendingMachines;
        public GameObject demineralization;
        
        private static PropManager _instance;

        public static PropManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        private void Start() {
            chairLift.SetActive(false);
        }

        public void ApplyEffect(int id) {
            switch (id) {
                case 1:
                    chairLift.GetComponent<Chairlift>().Enable();
                    chairLift.SetActive(true);
                    break;
                case 2:
                    vendingMachines.GetComponent<VendingMachines>().Upgrade();
                    break;
                case 3:
                    demineralization.GetComponent<Demineralization>().Upgrade();
                    break;
                    
            }
        }

        private void Update() {
            
        }
    }
}