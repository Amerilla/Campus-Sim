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
        private int _lastCall;
        private string _description;

        public Action(string name, int cost, List<Consequence> subConsequence, Consequence mainConsequence, Type type, int duration, int cooldown, int delay, string description) {
            _name = name;
            _cost = cost;
            _subConsequence = subConsequence;
            _mainConsequence = mainConsequence;
            _type = type;
            _duration = duration;
            _cooldown = cooldown;
            _delay = delay; // TODO: add delay function
            _description = description;
        }


        public void Execute(int currentTurn) {
            
            if(_lastCall + _duration + _cooldown < currentTurn){
                _mainConsequence.Apply();
                foreach (Consequence consequence in _subConsequence) {
                    consequence.Apply();
                }
            }
        }

        public enum Type
        {
            Positive, Negative, Random
        }
    }
}