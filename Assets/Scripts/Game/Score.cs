using Unity.VisualScripting;

namespace Game
{
    public class Score
    {
        private string _name;
        protected float _value;

        public Score(string name, float value) {
            _name = name;
            _value = value;
        }

        public void AddScore(float value) {
            _value += value;
        }

        public void SubScore(float value) {
            _value -= value;
        }

        public float GetScore() => _value;


    }

    public class SubScore:Score
    {
        private float _coeffcient;
        private Score _parentScore;
        public SubScore(string name, int value, float coefficient, Score parentScore) : base(name, value) {
            _coeffcient = coefficient;
            _parentScore = parentScore;
        }

        public void UpdateParentScore() {
            _parentScore.AddScore(base._value*_coeffcient);
        }

        public new void AddScore(float value) {
            base._value += value;
            UpdateParentScore();
        }
        
    }
    
}
