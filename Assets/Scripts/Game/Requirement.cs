namespace Game
{
    public class Requirement
    {
        private string _name;
        private int _rangeScoreValue;
        private int _importance;
        private SubScore _subScore;

        public Requirement(string name, int rangeScoreValue, int importance, SubScore subScore) {
            _name = name;
            _rangeScoreValue = rangeScoreValue;
            _importance = importance;
            _subScore = subScore;
        }
        
        
    }
}