using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Game
{
    public class Action
    {
        private string _name;
        private int _moneyChange;
        private List<Consequence> _consequences;
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
            _consequences = consequences;
            _duration = duration ?? 0;
            _cooldown = cooldown ?? 0;
            _delay = delay ?? 0;
        }

        public string GetName() => _name;

        public string GetDescription() => _description;

        public float GetMoneyChange() => _moneyChange;

        public float GetDuration() => _duration;

        public float GetCooldown() => _cooldown;

        public float GetDelay() => _delay;

        public void SetActionType(ActionType actionType) {
            _actionType = actionType;
        }

        public ActionType GetActionType() => _actionType;

        public void Execute(int currentTurn, Campus campus) {
            if (_lastCall + _duration + _cooldown >= currentTurn) return;
            if (!campus.Spend(-_moneyChange)) return;
            foreach (Consequence consequence in _consequences) {
                
                consequence.Apply();
            }
        }

        public enum ActionType
        {
            Positive, Negative, Random
        }
    }
}