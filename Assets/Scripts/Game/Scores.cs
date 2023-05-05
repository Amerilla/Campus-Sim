
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Game
{
    public class MainScore
    {
        private string _name;
        protected float _value;
        private float _mainCoeff;
        private List<SubScore> _subscores;

        [JsonConstructor]
        private MainScore(string name, float? mainCoeff, List<SubScore> scores) {
            _name = name;
            _value = 1;
            _mainCoeff = mainCoeff ?? 0;
            _subscores = scores;
        }

    }

    public class SubScore
    {
        private readonly string _name;
        private readonly float _subCoeff;
        private float _value;

        [JsonConstructor]
        private SubScore(string name, float subCoeff) {
            _name = name;
            _subCoeff = subCoeff;
            _value = 0;
        }

    }
    
    public enum MainCategory 
    {
        ENVIRONNEMENT, CULTURE, ECONOMIE, ENERGIE
    }

    public enum Environnement { POLLUTION, BIODIVERSITE }

    public enum Culture { DIVERSITE, VIE_ASSOCIATIVE }

    public enum Economie {
    }
    
}
