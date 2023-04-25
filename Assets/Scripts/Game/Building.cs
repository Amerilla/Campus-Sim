using System.Collections.Generic;

namespace Game
{
    public class Building
    {
        private string _name;
        private List<Score> _scores;
        private int _cost;
        private int _capacity;
        private int _resaleValue;
        private Owner _owner;
        private State _state;
        private Type _type;
        private enum State
        {
            Good, Ok, Abandoned
        }

        private enum Type
        {
            Transportation, Housing, Education, Parking, Park, Misc
        }
    }
}