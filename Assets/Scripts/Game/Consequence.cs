namespace Game
{
    public class Consequence
    {
        private SubScore _subScore;
        private int _value;

        public Consequence(SubScore subScore, int value) {
            _subScore = subScore;
            _value = value;
        }

        public void Apply() {
            _subScore.AddScore(_value);
        }
    }
}