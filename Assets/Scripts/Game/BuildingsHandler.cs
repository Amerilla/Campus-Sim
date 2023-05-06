using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class BuildingsHandler
    {
        private Dictionary<string, BuildingBehaviour> _buildingBehaviours = new();
        private Dictionary<string, BuildingStats> _buildingStats = new();

        public BuildingsHandler(IEnumerable<BuildingStats> buildingStats) {
            foreach (var buildingStat in buildingStats) {
                string name = buildingStat._name;
                _buildingBehaviours.Add(name, GameObject.Find(name).GetComponent<BuildingBehaviour>());
                _buildingStats.Add(name, buildingStat);
            }
        }

    }
}