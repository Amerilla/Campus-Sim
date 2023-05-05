using Newtonsoft.Json;

namespace Game
{
    public class Consequence
    {
        private SubScore _subScore;
        private int _value;

        [JsonConstructor]
        private Consequence(string subScore, int? value) {
            _subScore = new SubScore();
            _value = value ?? 0;
        }
        
        public Consequence(SubScore subScore, int value) {
            _subScore = subScore;
            _value = value;
        }

        public void Apply() {
            //_subScore.AddScore(_value);
        }
    }
}