using System.Collections.Generic;

namespace Game
{
    public class Population
    {
        private string _name;
        private int _budget;
        private int _amount;
        private List<Requirement> _requirements;
        private State _state;
        private enum State
        {
            Happy, Neutral, Angry
        }
    }
}