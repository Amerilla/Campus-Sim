using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class ChoiceGenerator
    {
        private Dictionary<ScoreType, List<Choice>> _choices;

        public ChoiceGenerator(Dictionary<ScoreType, List<Choice>> choices) {
            _choices = choices;
        }
    }
}