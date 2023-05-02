using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Game
{
    public class BuildingBehaviour:MonoBehaviour
    {
        public string _name;
        private BuildingStats _buildingStats;
        
        void Start() {
            _buildingStats = Buildings.get[_name];
        }

        void Update() {
            
        }
    }
}