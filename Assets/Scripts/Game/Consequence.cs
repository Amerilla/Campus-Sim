using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class Consequence
    {
        private readonly Score _score;
        private readonly int _value;

        [JsonConstructor]
        private Consequence(string score, int? value) {
            foreach (string oldSubScore in Action.OldSubScores()) {
                if (score.Equals(oldSubScore, StringComparison.OrdinalIgnoreCase)) {
                    throw new Exception($"ERROR! A subscore name was found ('{oldSubScore}'). We don't use those anymore! Use one of the following: 'Envrionnement', " +
                                        "'Energie', 'Mobilite', 'Academique', 'Economie', 'Energie', 'Culture'");
                }
            }

            _score = GameObject.Find("Game Manager").GetComponent<GameManager>().GetScore(Utilities.RemoveAccents(score));
            _value = value ?? 0;
        }

        public void Apply() {
            _score.AddScore(_value);
        }
        
    }
}