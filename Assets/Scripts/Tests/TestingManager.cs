using System;
using System.Collections.Generic;
using Data;
using Game;
using UnityEngine;
using Action = Game.Action;

namespace DefaultNamespace
{
    public class TestingManager : MonoBehaviour
    {
        private void Start() {
            DataRecorder recorder = new DataRecorder();
            HashSet<Score> scores1 = new() {
                new Score("Mobilité", 0, 25, 1),
                new Score("Environnement", 0, 50, 1),
                new Score("Culture", 0, 50, 1),
                new Score("Population", 0, 60, 1),
                new Score("Energie", 0, 20, 1),
                new Score("Economie", 0, 80, 1),
                new Score("Academique", 0, 10, 1),
            };
            HashSet<Action> actions1 = new() {
                new Action("CM devient CMigros", "description", null, null, 0, 5, 2, 1, null),
                new Action("Eau chaude dans les toilettes", "description2", null, null, 2, 3, 2, 1, null),
            };
            HashSet<Score> scores2 = new() {
                new Score("Mobilité", 0, 35, 1),
                new Score("Environnement", 0, 50, 1),
                new Score("Culture", 0, 40, 1),
                new Score("Population", 0, 60, 1),
                new Score("Energie", 0, 30, 1),
                new Score("Economie", 5, 80, 1),
                new Score("Academique", 10, 10, 1),
            };
            HashSet<Action> actions2 = new() {
                new Action("Parking a vélo payant", null, null, null, 2, 2, 2, 1, null),
                new Action("Toilettes payantes", null, null, null, 0, 4, 3, 1, null)
            };
            recorder.RecordScores(scores1, 1);
            recorder.RecordActions(actions1, 1);
            recorder.RecordScores(scores1, 2);
            recorder.RecordActions(actions1, 2);
            Debug.Log(recorder.SerializeJSON());
        }
    }
}