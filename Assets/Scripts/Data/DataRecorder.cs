using System;
using System.Collections.Generic;
using Game;
using Newtonsoft.Json;
using Action = Game.Action;

namespace Data
{
    public class DataRecorder
    {
        private DateTime _startDate;
        private DateTime _endTime;
        private Dictionary<int, HashSet<Action>> _actions;
        private Dictionary<int, HashSet<Score>> _scores;
        private Dictionary<int, (string,string)> _successes;

        public DataRecorder() {
            _startDate = DateTime.Now;
            _actions = new();
            _scores = new();
            _successes = new();
        }

        public void RecordActions(HashSet<Action> recorded, int currentTurn) {
            if (!_actions.ContainsKey(currentTurn)) {
                _actions[currentTurn] = recorded;
            } else {
                _actions[currentTurn].UnionWith(recorded);
            }
        }

        public void RecordScores(HashSet<Score> recorded, int currentTurn) {
            if (!_scores.ContainsKey(currentTurn)) {
                if (recorded.Count != Enum.GetValues(typeof(ScoreType)).Length) {
                    throw new ArgumentException(
                        $"There must be {Enum.GetValues(typeof(ScoreType)).Length} scores, but there was {recorded.Count}");
                }
                _scores[currentTurn] = recorded;
            } else {
                throw new ArgumentException($"Current turn has already been recorded!");
            }
        }

        public void RecordSuccess((string,string) recorded, int currentTurn) {
            _successes[currentTurn] = recorded;
        }
        
        public HashSet<Score> GetScoresAt(int turn) {
            if (_scores.ContainsKey(turn)) {
                return _scores[turn];
            }

            throw new ArgumentException($"Turn {turn} has not been recorded yet!");
        }
        
        public HashSet<Action> GetActionsAt(int turn) {
            if (_actions.ContainsKey(turn)) {
                return _actions[turn];
            }

            throw new ArgumentException($"Turn {turn} has not been recorded yet!");
        }

        public Dictionary<int,(string,string)> GetSucesses => _successes;

        public Dictionary<int, HashSet<Score>> GetScores => _scores;

        public Dictionary<int, HashSet<Action>> GetActions => _actions;

        public string SerializeJSON() {
            _endTime = DateTime.Now;
            var data = new {
                StartDate = _startDate,
                EndTime = _endTime,
                Actions = _actions,
                Scores = _scores
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            return json;
        }
    }
}