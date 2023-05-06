using System.Collections.Generic;

namespace Game
{
    public class PopulationStats
    {
        private string _name;
        private int _budget;
        private int _amount;
        private List<Requirement> _requirements;
        private State _state;

        public PopulationStats(string name, int budget, int amount, List<Requirement> requirements) {
            _name = name;
            _budget = budget;
            _amount = amount;
            _requirements = requirements;
            //_state = state;
        }

        public enum State
        {
            Happy, Neutral, Angry
        }
    }
}