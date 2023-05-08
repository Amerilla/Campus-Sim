namespace Game
{
    public class ScoreCategories
    {
        
        public abstract class MainCategory 
        {

        }

        public class ENVIRONNEMENT : MainCategory
        {
            public class POLLUTION {}
            
            public class BIODIVERSITE {}
        }

        public class CULTURE : MainCategory
        {
            public class DIVERSITE {}
            
            public class VIE_ASSOCIATIVE {}
        }

        public class ECONOMIE : MainCategory
        {
            public class CHIFFRE_AFFAIRE {}
            
            public class VARIETE_OFFRE {}
        }

        public class ENERGIE : MainCategory
        {
            public class CONSOMMATION {}
            
            public class PRODUCTION {}
        }

        public class ACADEMIQUE : MainCategory
        {
            public class RESULTAT {}
            
            public class PUBLICATION {}
        }

        public class POPULATION : MainCategory
        {
            public class BIEN_ETRE {}
            
            public class MINORITES {}
        }

        public class MOBILITE : MainCategory
        {
            public class MOBILITE_INDIVIDUELLE {}
            
            public class TRANSPORTS_PUBLICS {}
        }
        
    }
}