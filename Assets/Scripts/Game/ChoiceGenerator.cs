using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class ChoiceGenerator
    {
        private List<List<Choice>> _choices;

        public ChoiceGenerator(params List<Choice>[] choices) {
            _choices = choices.ToList();
        }
    }
}