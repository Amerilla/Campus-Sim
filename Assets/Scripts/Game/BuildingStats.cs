using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Game
{
    public class BuildingStats
    {
        public const int DEFAULT_COST = 50;
        public const int DEFAULT_CAPACITY = 1000;
        public const int DEFAULT_RESALEVALUE = 20000;
        public const Type DEFAULT_TYPE = Type.Education;
        
        public string _name;
        private List<MainScore> _scores;
        private int _cost;
        private int _capacity;
        private int _resaleValue;
        private string _description;
        private Owner _owner;
        private State _state;
        private Type _type;

        
        [JsonConstructor]
        private BuildingStats(string name, [CanBeNull] string description, int? cost, int? capacity, int? resaleValue, [CanBeNull] string buildingType) {
            _name = name;
            _description = description ?? "No description provided";
            _cost = cost ?? DEFAULT_COST;
            _capacity = capacity ?? DEFAULT_CAPACITY;
            _resaleValue = resaleValue ?? DEFAULT_RESALEVALUE;
            _owner = null;
            _scores = new List<MainScore>();

            if (buildingType == null) {
                _type = DEFAULT_TYPE;
            } else {
                try {
                    _type = (Type)Enum.Parse(typeof(Type), buildingType);
                } catch {
                    throw new Exception($"buildingType: {buildingType} does not correspond to any Type value");
                }
            }

            
        }
        
        public BuildingStats(string name, List<MainScore> scores, int cost, int capacity, int resaleValue, string description, Owner owner, State state, Type type) {
            _name = name;
            _scores = scores;
            _cost = cost;
            _capacity = capacity;
            _resaleValue = resaleValue;
            _description = description;
            _owner = owner;
            _state = state;
            _type = type;
        }

        
        
        public enum State
        {
            Good, Ok, Abandoned
        }
        public enum Type
        {
            Transportation, Housing, Education, Parking, Park, Misc
        }

        public int GetResaleValue() => _resaleValue;

    }
    
}