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

               public Campus(string name, int overallScore, int balance, int netWorth, int revenue, int expenses, int population, State state) {
                   _name = name;
                   _overallScore = overallScore;
                   _balance = balance;
                   _netWorth = netWorth;
                   _revenue = revenue;
                   _expenses = expenses;
                   _population = population;
                   _state = state;
               }

               public enum State
               {
                   Good, Neutral, Crisis
               } 
               
               
    }
}