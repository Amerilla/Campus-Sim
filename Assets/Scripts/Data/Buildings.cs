using System.Collections.Generic;
using System.Xml;
using Game;

namespace Assets.Scripts.Data
{
    public static class Buildings
    {
        public static BuildingStats IN = new BuildingStats("IN", new List<Score>(), 0, 0, 0,
            "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        public static BuildingStats BM = new BuildingStats("BM", new List<Score>(), 0, 0, 0,
            "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        public static BuildingStats CM = new BuildingStats("CM", new List<Score>(), 0, 0,
            0, "", null, BuildingStats.State.Good, BuildingStats.Type.Education);

        public static Dictionary<string, BuildingStats> get = new Dictionary<string, BuildingStats>() {

            { "IN",IN},
            {"BM",BM},
            {"CM",CM},
        };

    };

}
