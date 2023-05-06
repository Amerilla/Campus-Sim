using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Game
{
    public class Action
    {
        private string _name;
        private int _moneyChange;
        private List<Consequence> _subConsequence;
        private Consequence _mainConsequence;
        private ActionType _actionType;
        private int _duration;
        private int _cooldown;
        private int _delay;
        private int _lastCall;
        private string _description;

        [JsonConstructor]
        private Action(string name, [CanBeNull] string description, int? moneyChange, List<Consequence> consequences, int? duration, int? cooldown, int? delay) {
            _name = name;
            _description = description;
            _moneyChange = moneyChange ?? 0;
            _duration = duration ?? 0;
            _cooldown = cooldown ?? 0;
            _delay = delay ?? 0;
        }
        
        public Action(string name, int moneyChange, List<Consequence> subConsequence, Consequence mainConsequence, ActionType actionType, int duration, int cooldown, int delay, string description) {
            _name = name;
            _description = description;
            _moneyChange = moneyChange;
            _subConsequence = subConsequence;
            _mainConsequence = mainConsequence;
            _actionType = actionType;
            _duration = duration;
            _cooldown = cooldown;
            _delay = delay; // TODO: add delay function
            _description = description;
        }

        public void SetActionType(ActionType actionType) {
            _actionType = actionType;
        }

        public ActionType GetActionType() => _actionType;

        public void Execute(int currentTurn, Campus campus) {
            if (_lastCall + _duration + _cooldown >= currentTurn) return;
            if (!campus.Spend(_moneyChange)) return;
            _mainConsequence.Apply();
            foreach (Consequence consequence in _subConsequence) {
                consequence.Apply();
            }
        }

        public enum ActionType
        {
            Positive, Negative, Random
        }
    }
}