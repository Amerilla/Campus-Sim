
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Game
{
    public class ScoresHandler
    {
        private readonly Dictionary<string, MainScore> _mainScores;

        public ScoresHandler(IEnumerable<MainScore> scores) {
            _mainScores = new Dictionary<string, MainScore>();
            foreach (var mainScore in scores) {
                _mainScores.Add(mainScore._name, mainScore);
            }
        }

        public List<float> GetScores() {
            return new List<float>() {
                GetScore("Environnement"),
                GetScore("Population"),
                GetScore("Académique"),
                GetScore("Culture"),
                GetScore("Energie"),
                GetScore("Economie"),
                GetScore("Mobilité")
            };
        }
        
        public float GetScore(string subScoreName) {
            foreach (var (mainName, mainScore) in _mainScores) {
                if (mainName == subScoreName) return mainScore._value;
                foreach (var (subName, subScore) in mainScore._subscores) {
                    if (subName == subScoreName) return subScore._value;
                }
            }

            throw new ArgumentException($"{subScoreName} is not the name of a SubScore nor a MainScore");

        }
    }
    
    public class MainScore
    {
        internal string _name;
        internal float _value;
        internal float _mainCoeff;
        internal Dictionary<string, SubScore> _subscores;

        [JsonConstructor]
        private MainScore(string name, float? mainCoeff, List<SubScore> scores) {
            _name = name;
            _value = 1;
            _mainCoeff = mainCoeff ?? 0;
            _subscores = new Dictionary<string, SubScore>();
            foreach (var subScore in scores) {
                _subscores.Add(subScore._name, subScore);
            }
        }

    }

    public class SubScore
    {
        internal readonly string _name;
        internal readonly float _subCoeff;
        internal float _value;

        [JsonConstructor]
        private SubScore(string name, float subCoeff) {
            _name = name;
            _subCoeff = subCoeff;
            _value = 0;
        }

    }
    
    public enum MainCategory 
    {
        ENVIRONNEMENT, CULTURE, ECONOMIE, ENERGIE, ACADEMIQUE, POPULATION, MOBILITE
    }

    public enum Environnement { POLLUTION, BIODIVERSITE }

    public enum Culture { DIVERSITE, VIE_ASSOCIATIVE }

    public enum Economie { CHIFFRE_D_AFFAIRE, VARIETE_DE_L_OFFRE}
    
    public enum Energie { CONSOMMATION, PRODUCTION}
    
    public enum Academique { RESULTAT, PUBLICATION}
    
    public enum Population { BIEN_ETRE, MINORITES}
    
    public enum Mobilite { MOBILITE_INDIVIDUELLE, TRANSPORTS_PUBLICS}
    
}
