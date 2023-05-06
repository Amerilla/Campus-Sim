
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
    }
    
    public class MainScore
    {
        internal string _name;
        internal float _value;
        internal float _mainCoeff;
        internal List<SubScore> _subscores;

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
