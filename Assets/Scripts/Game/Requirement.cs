namespace Game
{
    public class Requirement
    {
        private string _name;
        private int _rangeScoreValue;
        private int _importance;
        private Score _subScore;

        public Requirement(string name, int rangeScoreValue, int importance, Score subScore) {
            _name = name;
            _rangeScoreValue = rangeScoreValue;
            _importance = importance;
            _subScore = subScore;
        }
        
        
    }
}