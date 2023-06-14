using System;
using UnityEngine;

namespace Props
{
    public class PropManager : MonoBehaviour
    {
        public GameObject chairLift;
        public GameObject vendingMachines;
        public GameObject demineralization;
        public GameObject publicPlazas;
        public GameObject airConditioning;
        public GameObject solarPanels;
        public GameObject golfCarts;
        
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
                case 4:
                    publicPlazas.GetComponent<PublicPlazas>().Upgrade();
                    break;
                case 5:
                    airConditioning.GetComponent<AirConditioning>().Upgrade();
                    break;
                case 6:
                    solarPanels.GetComponent<SolarPanels>().Upgrade();
                    break;
                case 7:
                    golfCarts.GetComponent<GolfCarts>().Upgrade();
                    break;
            }
        }

        private void Update() {
            
        }
    }
}