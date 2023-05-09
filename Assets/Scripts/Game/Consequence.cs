using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class Consequence
    {
        private readonly SubScore _subScore;
        private readonly int _value;

        [JsonConstructor]
        private Consequence(string subScore, int? value) {
            _subScore = GameObject.Find("Game Manager").GetComponent<GameManager>().GetSubScore(subScore);
            _value = value ?? 0;
        }
        
        public Consequence(SubScore subScore, int value) {
            _subScore = subScore;
            _value = value;
        }

        public void Apply() {
           // _subScore.AddScore(_value);
        }
    }
}