using Assets.Scripts.Data;
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
            if (positive.ActionType() != Action.Type.Positive || random.ActionType() != Action.Type.Random || random.ActionType() != Action.Type.Negative) {
                throw new JsonException("ERROR. Mismatch between declared action type and actual type attribute. For example, the json object" +
                                        "may contain 'positive' while its type is 'negative'");
            }
            _positiveAction = positive;
            _randomAction = random;
            _negativeAction = negative;
        }
    }
}