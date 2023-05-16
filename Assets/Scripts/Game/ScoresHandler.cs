
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

        public void UpdateScores() {
            foreach (var score in _scores) {
                score.Value.UpdateScore();
            }
        }
        
    }
    
    public class Score
    {
        private readonly string _name;
        private int _value;
        private int _byTurn;
        private int _coefficient;

        [JsonConstructor]
        private Score(string name, int? byturn, int? initialValue) {
            _name = Utilities.RemoveAccents(name);
            _byTurn = byturn ?? 0;
            _value = initialValue ?? 50;
        }

        public void AddScore(int added) {
            _value += added*_coefficient;
        }

        public void AddByTurn(int added) {
            _byTurn += added*_coefficient;
        }
        public int GetValue() => _value;

        public void SetScore(int score) {
            _value = score*_coefficient;
        }

        public void UpdateScore() {
            _value += _byTurn;
        }

        public string GetName() => _name;

        public (int, int) GetCurrentAndNextScore => (_value, _value + _byTurn);
    }
    
    public enum ScoreType
    {
        CULTURE, ECONOMIE, POPULATION, MOBILITE, ACADEMIQUE, ENVIRONNEMENT, ENERGIE
    }

}
