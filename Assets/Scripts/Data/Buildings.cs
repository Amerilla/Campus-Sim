using System.Collections.Generic;
using System.Xml;
using Game;

namespace Assets.Scripts.Data
{
    public static class Buildings
    {
        public static BuildingStats EPFL_IN = new BuildingStats("EPFL_IN", new List<Score>(), 0, 0, 0,
            "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        public static BuildingStats EPFL_BM = new BuildingStats("EPFL_BM", new List<Score>(), 0, 0, 0,
            "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        public static BuildingStats EPFL_CM = new BuildingStats("EPFL_CM", new List<Score>(), 0, 0,
            0, "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        public static Dictionary<string, BuildingStats> get = new Dictionary<string, BuildingStats>() {

            { "IN",EPFL_IN},
            {"BM",EPFL_BM},
            {"CM",EPFL_CM},
        };

        private List<string> names;
        
        

    };

}
