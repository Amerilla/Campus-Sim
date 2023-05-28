using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace Game
{
    
    public class SuccessRequirement
    {
        private readonly Score _score;
        private int _minRequired;
        private int _maxRequired;
        private int _turns;
        private int _since = 0;
        private bool _activated = false;
        
        [JsonConstructor]
        private SuccessRequirement(string score, int? minRequired, int? maxRequired, int turns)
        {
            foreach (string oldSubScore in Action.OldSubScores()) {
                if (score.Equals(oldSubScore, StringComparison.OrdinalIgnoreCase)) {
                    throw new Exception($"ERROR! A subscore name was found ('{oldSubScore}'). We don't use those anymore! Use one of the following: 'Envrionnement', " +
                                        "'Energie', 'Mobilite', 'Academique', 'Economie', 'Energie', 'Culture'");
                }
            }

            _score = GameObject.Find("Game Manager").GetComponent<GameManager>().GetScoreFromJSON(Utilities.RemoveAccents(score));
            _minRequired = minRequired ?? 0;
            _maxRequired = maxRequired ?? 100;
            _turns = turns;
        }
        
        
        
        public bool HasRequirement() {
            if (!_activated) {
                if (_score.GetValue() >= _minRequired && _score.GetValue() <= _maxRequired) {
                    if (_since == _turns) {
                        _since = 0;
                        _activated = true;
                        return true;
                    }

                    _since++;
                }
            }

            return false;
        }

    }
    
    public class SuccessBehaviour
    {
        private string _name;
        private string _description;
        private List<SuccessRequirement> _requirements;
        [JsonConstructor]
        private SuccessBehaviour(string name, string description, List<SuccessRequirement> requirements)
        {
            _name = name;
            _description = description;
            _requirements = requirements;
        }

        public (string,string) HasSuccess() {
            foreach (var requirement in _requirements) {
                if (requirement.HasRequirement()) {
                    return (_name, _description);
                }
            }

            return (null, null);
        }
    }
}