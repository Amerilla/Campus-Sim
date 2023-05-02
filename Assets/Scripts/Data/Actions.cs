
using System.Collections.Generic;
using Game;

namespace Assets.Scripts.Data
{
    public class Actions
    {
        private static Action Action_1 = new Action("action 1", 0,null, null,
            Action.Type.Positive, 0, 0, 0, "test");

        private static Action Action_2 = new Action("action 2", 0, null, null,
            Action.Type.Negative, 0, 0, 0, "test");
    }
}