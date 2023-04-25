using System;
using System.Collections.Generic;

namespace Game
{
    public class BuildingStats
    {
        private string _name;
        private List<Score> _scores;
        private int _cost;
        private int _capacity;
        private int _resaleValue;
        private string _description;
        private Owner _owner;
        private State _state;
        private Type _type;

        public BuildingStats(string name, List<Score> scores, int cost, int capacity, int resaleValue, string description, Owner owner, State state, Type type) {
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

    }
    
}