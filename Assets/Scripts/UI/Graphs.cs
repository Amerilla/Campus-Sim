using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace UI
{
    public class Graphs : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start() {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            root.style.height = new StyleLength(Length.Percent(100));
            root.style.height = new StyleLength(Length.Percent(100));
            // Data to display on the graph
            List<Vector2> dataPoints = new List<Vector2>
            {
                new Vector2(0, 50),
                new Vector2(1, 70),
                new Vector2(2, 30),
                new Vector2(3, 60),
                new Vector2(4, 10),
                new Vector2(5, 40),
                new Vector2(6, 80),
                new Vector2(7, 20),
                new Vector2(8, 90)
            };

            // Create a container for the graph
            var graphContainer = new VisualElement();
            graphContainer.style.width = new StyleLength(Length.Percent(100));
            graphContainer.style.height = new StyleLength(Length.Percent(100));
            graphContainer.style.backgroundColor = Color.black;
            float width = graphContainer.layout.width;
            float height = graphContainer.layout.height;
            // Create the points using VisualElement
            VisualElement lastPoint = null;

            foreach (Vector2 dataPoint in dataPoints)
            {
                var point = new VisualElement();
                point.style.position = Position.Absolute;
                point.style.width = 10;
                point.style.height = 10;
                point.style.backgroundColor = Color.white;
                var pointX = (1920.0f / dataPoints.Count) * dataPoint.x;
                var pointY = (1080.0f/ dataPoints.Count) * dataPoint.y/10;
                point.style.left = new StyleLength(pointX);
                point.style.top = new StyleLength(pointY);
                graphContainer.Add(point);
                // Create a line between the last point and the current point
                
                if (lastPoint != null)
                {
                    var line = new VisualElement();
                    line.style.position = Position.Absolute;
                    line.style.width = 3;
                    line.style.height = 3;
                    line.style.backgroundColor = Color.white;
                    
                    graphContainer.Add(line);
                }

                lastPoint = point;
            }

            root.Add(graphContainer);


        }

        // Update is called once per frame
        void Update() {

        }
    }
}
