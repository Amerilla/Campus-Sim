using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Game
{
    public class Action
    {
        private string _name;
        private List<Consequence> _consequences;
        private ActionType _actionType;
        private List<Requirement> _requirements;
        private int _duration;
        private int _cooldown;
        private int _delay;
        private int _lastCall;
        private string _description;

        [JsonConstructor]
        private Action(string name, [CanBeNull] string description, List<Consequence> consequences, int? duration, int? cooldown, int? delay) {
            _name = name;
            _description = description;
            _consequences = consequences;
            _duration = duration ?? 0;
            _cooldown = cooldown ?? 0;
            _delay = delay ?? 0;
        }

        public string GetName() => _name;

        public string GetDescription() => _description;
        

        public float GetDuration() => _duration;

        public float GetCooldown() => _cooldown;

        public float GetDelay() => _delay;

        public void SetActionType(ActionType actionType) {
            _actionType = actionType;
        }

        public bool CanBeExecuted(int currentTurn) {
            if (_cooldown < 0 || _duration < 0) {
                return false;
            }
            return _delay + _duration + _cooldown <= _lastCall + currentTurn;
        }
        
        public List<Consequence> Execute(int currentTurn) {
            List<Consequence> remainingConsequences = new List<Consequence>();
            foreach (var requirement in _requirements) {
                if (!requirement.HasRequirement()) {
                    return remainingConsequences;
                }
            }
            if (CanBeExecuted(currentTurn)) {
                foreach (var consequence in _consequences) {
                    consequence.SetDelay(_delay);
                    consequence.SetRemainingTurns(_duration);
                    if (!consequence.Update()) {
                        remainingConsequences.Add(consequence);
                    }
                }
            }
            return remainingConsequences;
        }

        public ActionType GetActionType() => _actionType;

        public enum ActionType
        {
            Positive, Negative, Random
        }

        public static HashSet<string> OldSubScores() => new HashSet<string>() {
            "Pollution", "Biodiversité", "Vie associative", "Diversité", "Chiffre d'affaire", "Variété de l'offre",
            "Consommation", "Production",
            "Résultat", "Publication", "Bien-être", "Minorités", "Mobilité individuelle", "Transports publics"
        };
    }
}