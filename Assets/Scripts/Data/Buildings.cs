using System.Collections.Generic;
using System.Xml;
using Game;

namespace Assets.Scripts.Data
{
    public static class Buildings
    {
        private static BuildingStats EPFL_IN = new BuildingStats("EPFL_IN", new List<MainScore>(), 0, 0, 0,
            "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        private static BuildingStats EPFL_BM = new BuildingStats("EPFL_BM", new List<MainScore>(), 0, 0, 0,
            "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        private static BuildingStats EPFL_CM = new BuildingStats("EPFL_CM", new List<MainScore>(), 0, 0,
            0, "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        public static Dictionary<string, BuildingStats> get = new Dictionary<string, BuildingStats>() {

            { "EPFL_IN",EPFL_IN},
            {"EPFL_BM",EPFL_BM},
            {"EPFL_CM",EPFL_CM},
        };

        public static readonly List<string> names = new List<string>();

        static Buildings() {
            
            foreach (var (s, buildingStats) in get) {
                names.Add(s);
            }
        }
        

    };

}
