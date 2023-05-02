using System.Collections.Generic;

namespace Game
{
    public class Action
    {
        private string _name;
        private int _cost;
        private List<Consequence> _subConsequence;
        private Consequence _mainConsequence;
        private Type _type;
        private int _duration;
        private int _cooldown;
        private int _delay;
        private string _description;

        public Action(string name, int cost, List<Consequence> subConsequence, Consequence mainConsequence, Type type, int duration, int cooldown, int delay, string description) {
            _name = name;
            _cost = cost;
            _subConsequence = subConsequence;
            _mainConsequence = mainConsequence;
            _type = type;
            _duration = duration;
            _cooldown = cooldown;
            _delay = delay;
            _description = description;
        }


        public enum Type
        {
            Positive, Negative, Random
        }
    }
}