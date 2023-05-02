using System;
using Assets.Scripts.Data;

namespace Game
{
    public class Campus
    {
       private string _name;
       private float _overallScore;
       private int _balance;
       private int _revenue;
       private int _expenses;
       private int _population;
       private State _state;

       public Campus(string name, float overallScore, int balance, int revenue, int expenses, int population, State state) {
           _name = name;
           _overallScore = overallScore;
           _balance = balance;
           _revenue = revenue;
           _expenses = expenses;
           _population = population;
           _state = state;
       }

       public enum State
       {
           Good, Neutral, Crisis
       }

       public bool Spend(int cost) {
           if (_balance < cost) return false;
           _balance -= cost;
           return true;
       }

       public void Balance() {
           _balance = _balance - _expenses + _revenue;
       }

       public int NetWorth() {
           int sum = 0;
           foreach (var (s, buildingStats) in Buildings.get) {
               sum += buildingStats.GetResaleValue();
           }

           return _balance + sum;
       }

       public void ComputeOverallScore() {
           
           //TODO: Iterate on each individual scores
           
       }
    }
}