using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class Consequence
    {
        private Score _score;
        private readonly int _value;
        private readonly bool _isByTurn;
        private int _remainingTurns;
        private int _delayRemaining;
        private bool _applied = false;

        [JsonConstructor]
        private Consequence(string score, int? value, bool? isByTurn) {
            foreach (string oldSubScore in Action.OldSubScores()) {
                if (score.Equals(oldSubScore, StringComparison.OrdinalIgnoreCase)) {
                    throw new Exception($"ERROR! A subscore name was found ('{oldSubScore}'). We don't use those anymore! Use one of the following: 'Envrionnement', " +
                                        "'Energie', 'Mobilite', 'Academique', 'Economie', 'Energie', 'Culture'");
                }
            }

            _score = GameObject.Find("Game Manager").GetComponent<GameManager>().GetScoreFromJSON(Utilities.RemoveAccents(score));
            _value = value ?? 0;
            _isByTurn = isByTurn ?? false;
        }
        
        private void Apply() {
            if (_isByTurn) {
                if (!_applied) {
                    _score.AddByTurn(_value);
                    _applied = true;
                }

                if (_applied && _remainingTurns < 0) {
                    _score.AddByTurn(-_value);
                    _applied = false;
                }

            }
            else {
                if (!_applied) {
                    _score.AddScore(_value);
                    _applied = true;
                }

                if (_applied && _remainingTurns < 0)
                    _applied = false;
            }
            
        }

        public bool isByTrun() => _isByTurn;
        
        public void SetDelay(int delay) {
            _delayRemaining = delay;
        }

        public void SetRemainingTurns(int turns) {
            _remainingTurns = turns;
        }

        /*
         * Update function return true if the consequence is finished
         */
        public bool Update() {
            if (_delayRemaining > 0) {
                _delayRemaining -= 1;
                return false;
            }
            Apply();
            if (_remainingTurns < 0) {
                return true;
            }
            _remainingTurns -= 1;
            return false;
        }

        public int GetValue() => _value;

        public ScoreType? GetConsequenceScoreType() => _score.GetScoreType();
    }
}