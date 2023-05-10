
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;


namespace Game
{
    public class ScoresHandler
    {
        private readonly Dictionary<string, Score> _scores;
        

        public ScoresHandler(List<Score> scores) {
            _scores = new Dictionary<string, Score>();
            foreach (Score score in scores) {
                _scores.Add(Utilities.RemoveAccents(score.GetName()), score);
            }
        }

        public Score GetScore(string score) {
            return _scores[Utilities.RemoveAccents(score)];
        }
        
    }
    
    public class Score
    {
        internal string _name;
        internal float _value;
        internal float _coeff;

        [JsonConstructor]
        private Score(string name, float? coefficient, float? initialValue) {
            _name = Utilities.RemoveAccents(name);
            _coeff = coefficient ?? 0;
            _value = initialValue ?? 50;
        }

        public void AddScore(float added) {
            _value += added;
        }
        public float GetScore => _value;

        public void SetScore(float score) {
            _value = score;
        }

        public string GetName() => _name;
        
    }
    
    public enum ScoreType
    {
        CULTURE, ECONOMIE, POPULATION, MOBILITE, ACADEMIQUE, ENVIRONNEMENT, ENERGIE
    }

}
