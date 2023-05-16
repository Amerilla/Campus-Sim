using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Game
{
    public class Campus
    { 
        private BuildingsHandler _buildingsHandler;
        private string _name;
        private State _state;

       public Campus(string name, float overallScore, int balance, int revenue, int expenses, int population, State state, BuildingsHandler buildingsHandler) {
           _name = name;
           _state = state;
           _buildingsHandler = buildingsHandler;
       }

       public enum State
       {
           Good, Neutral, Crisis
       }
       
       public void ComputeOverallScore() {
           
           //TODO: Iterate on each individual scores
           
       }
       
       
    }
}