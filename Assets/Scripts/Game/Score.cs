using Unity.VisualScripting;

namespace Game
{
    public class Score
    {
        private string _name;
        private int _value;
        private float _coefficient;

        public Score(string name, int value, float coefficient) {
            _name = name;
            _value = value;
            _coefficient = coefficient;
        }

        public void AddScore(int value) {
            _value += value;
        }

        public void SubScore(int value) {
            _value -= value;
        }

        public int GetScore() => _value;
        

    }

    public class SubScore:Score
    {
        public SubScore(string name, int value, float coefficient) : base(name, value, coefficient) {
        }
    }
    
}
