using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class ChoiceGenerator
    {
        private Dictionary<MainCategory, List<Choice>> _choices;

        public ChoiceGenerator(Dictionary<MainCategory, List<Choice>> choices) {
            _choices = choices;
        }
    }
}