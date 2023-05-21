using Newtonsoft.Json;

namespace Game
{
    public class Choice
    {
        private Action _positiveAction;
        private Action _randomAction;
        private Action _negativeAction;

        [JsonConstructor]
        private Choice(Action positive, Action random, Action negative) {
            _positiveAction = positive;
            _randomAction = random;
            _negativeAction = negative;
            _positiveAction.SetActionType(Action.ActionType.Positive);
            _randomAction.SetActionType(Action.ActionType.Random);
            _negativeAction.SetActionType(Action.ActionType.Negative);
        }

        public Action GetPositive() => _positiveAction;

        public Action GetRandom() => _randomAction;

        public Action GetNegative() => _negativeAction;

        public void Reset() {
            _positiveAction.Reset();
            _randomAction.Reset();
            _negativeAction.Reset();
        }
    }
}