
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
//using Unity.VisualScripting;
using UnityEngine;



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
        private int _coefficient = 1 ;

        [JsonConstructor]
        private Score(string name, int? byturn, int? initialValue, int? coefficient) {
            _name = Utilities.RemoveAccents(name);
            _byTurn = byturn ?? 0;
            _value = initialValue ?? 50;
            _coefficient = coefficient ?? 1;
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

        public ScoreType? GetScoreType() {
            if (_name.Equals(ScoreType.ENERGIE.ToString(),StringComparison.OrdinalIgnoreCase))
                return ScoreType.ENERGIE;
            if (_name.Equals(ScoreType.ECONOMIE.ToString(),StringComparison.OrdinalIgnoreCase))
                return ScoreType.ECONOMIE;
            if (_name.Equals(ScoreType.ENVIRONNEMENT.ToString(),StringComparison.OrdinalIgnoreCase))
                return ScoreType.ENVIRONNEMENT;
            if (_name.Equals(ScoreType.POPULATION.ToString(),StringComparison.OrdinalIgnoreCase))
                return ScoreType.POPULATION;
            if (_name.Equals(ScoreType.CULTURE.ToString(),StringComparison.OrdinalIgnoreCase))
                return ScoreType.CULTURE;
            if (_name.Equals(ScoreType.ACADEMIQUE.ToString(),StringComparison.OrdinalIgnoreCase))
                return ScoreType.ACADEMIQUE;
            if (_name.Equals(ScoreType.MOBILITE.ToString(),StringComparison.OrdinalIgnoreCase))
                return ScoreType.MOBILITE;
            
            return null;
        }

        public (int, int) GetCurrentAndNextScore() => (_value, _value + _byTurn);
    }
    
    public enum ScoreType
    {
        CULTURE, ECONOMIE, POPULATION, MOBILITE, ACADEMIQUE, ENVIRONNEMENT, ENERGIE
    }

}
