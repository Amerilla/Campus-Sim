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
        private int duration;
        private int cooldown;
        private int delay;
        private string description;
        
        private enum Type
        {
            Positive, Negative, Random
        }
    }
}