using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;


namespace Game
{
    public class Requirement
    {
        private readonly Score _score;
        private readonly int _value;

        [JsonConstructor]
        private Requirement(string score, int? minRequired) {
            foreach (string oldSubScore in Action.OldSubScores()) {
                if (score.Equals(oldSubScore, StringComparison.OrdinalIgnoreCase)) {
                    throw new Exception($"ERROR! A subscore name was found ('{oldSubScore}'). We don't use those anymore! Use one of the following: 'Envrionnement', " +
                                        "'Energie', 'Mobilite', 'Academique', 'Economie', 'Energie', 'Culture'");
                }
            }

            _score = GameObject.Find("Game Manager").GetComponent<GameManager>().GetScore(Utilities.RemoveAccents(score));
            _value = minRequired ?? 0;
        }
        
        
        
        
        public bool HasRequirement() {
            return _score.GetValue() >= _value;
        }

        public int GetRequirement() => _value;

        public ScoreType? GetRequirementScoreType() => _score.GetScoreType();
    }
}