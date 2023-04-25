namespace Game
{
    public class Campus
    {
       private string _name;
               private int _overallScore;
               private int _balance;
               private int _netWorth;
               private int _revenue;
               private int _expenses;
               private int _population;
               private State _state;
               private enum State
               {
                   Good, Neutral, Crisis
               } 
    }
}