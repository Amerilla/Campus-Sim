using UnityEditor;
using UnityEngine;

namespace TechnoBabelGames
{
    public class TBLineRendererTool : EditorWindow
    {
        string lineName = "New Line Renderer";
        int linePoints = 2;
        float lineWidth = 0.5f;
        Color startColor = Color.white;
        Color endColor = Color.white;
        enum Alignment { FaceCamera, X, Y, Z }
        Alignment alignment;
        Material lineMaterial;
        enum LineColoring { SolidColor, Gradient, Texture }
        LineColoring lineColoring;
        enum LineMaterial { Stretch, Tile }
        LineMaterial lineMaterialEnum;
        enum LinePointsShape { Custom, Line, Triangle, Square, Pentagon, Hexagon, Heptagon, Octagon, Nonagon, Decagon }
        LinePointsShape linePointsShape;
        bool roundCorners;
        bool roundEndCaps;
        bool closeLineLoop = false;
        float shapeSize = 3;

        [MenuItem("Tools/TechnoBabelGames/Easy Line Renderer")]
        public static void ShowWindow()
        {
            TBLineRendererTool window = (TBLineRendererTool)GetWindow(typeof(TBLineRendererTool), false, "Easy Line Renderer");
            window.minSize = new Vector2(400, 400);
        }

        private void OnGUI()
        {
            DrawUILine(Color.gray);
            GUILayout.Space(8);
            GUILayoutLineVisualProperties();
            GUILayout.Space(8);
            DrawUILine(Color.gray);
            GUILayout.Space(8);
            GUILayoutLineColoring();
            GUILayout.Space(8);
            DrawUILine(Color.gray);
            GUILayout.Space(8);
            GUILayoutLineTechnicalProperties();
            GUILayout.Space(8);
            DrawUILine(Color.gray);
            GUILayout.Space(8);
            GUILayoutButton();
            GUILayout.Space(8);
            DrawUILine(Color.gray);
        }

        //Width & Rounded Corners/End Caps
        void GUILayoutLineVisualProperties()
        {
            EditorGUILayout.LabelField("Visual Properties", EditorStyles.boldLabel);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            lineWidth = EditorGUILayout.Slider("Line width", lineWidth, 0.01f, 1);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            roundEndCaps = EditorGUILayout.Toggle("Rounded End Caps", roundEndCaps);
            roundCorners = EditorGUILayout.Toggle("Rounded Corners", roundCorners);
            EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;
        }

        void GUILayoutLineColoring()
        {
            EditorGUILayout.LabelField("Line Coloring", EditorStyles.boldLabel);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            lineColoring = (LineColoring)EditorGUILayout.EnumPopup("Coloring Type", lineColoring);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            switch (lineColoring)
            {
                case LineColoring.SolidColor:
                    startColor = EditorGUILayout.ColorField("Line Color", startColor);
                    break;
                case LineColoring.Gradient:
                    startColor = EditorGUILayout.ColorField("Start Color", startColor);
                    endColor = EditorGUILayout.ColorField("End Color", endColor);
                    break;
                case LineColoring.Texture:
                    lineMaterial = (Material)EditorGUILayout.ObjectField("Material", lineMaterial, typeof(Material), false);
                    if (lineMaterial == null)
                    {
                        EditorGUILayout.HelpBox("Material is required", MessageType.Info, false);
                    }
                    lineMaterialEnum = (LineMaterial)EditorGUILayout.EnumPopup("Mode", lineMaterialEnum);
                    break;
                default:
                    break;
            }
            EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;
        }

        void GUILayoutLineTechnicalProperties()
        {
            EditorGUILayout.LabelField("Technical Properties", EditorStyles.boldLabel);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            linePointsShape = (LinePointsShape)EditorGUILayout.EnumPopup("Starting Shape", linePointsShape);

            switch (linePointsShape)
            {
                case LinePointsShape.Custom:
                    GUILayout.Space(8);

                    EditorGUI.indentLevel++;
                    linePoints = EditorGUILayout.IntField("Points", linePoints);
                    if (linePoints < 2)
                        linePoints = 2;
                    else if (linePoints > 10)
                        linePoints = 10;

                    GUILayout.Space(8);

                    closeLineLoop = EditorGUILayout.Toggle("Close Loop", closeLineLoop);
                    EditorGUI.indentLevel--;
                    break;
                case LinePointsShape.Line:
                    GUILayoutShapeSize();
                    linePoints = 2;
                    closeLineLoop = false;
                    break;
                case LinePointsShape.Triangle:
                    GUILayoutShapeSize();
                    linePoints = 3;
                    closeLineLoop = true;
                    break;
                case LinePointsShape.Square:
                    GUILayoutShapeSize();
                    linePoints = 4;
                    closeLineLoop = true;
                    break;
                case LinePointsShape.Pentagon:
                    GUILayoutShapeSize();
                    linePoints = 5;
                    closeLineLoop = true;
                    break;
                case LinePointsShape.Hexagon:
                    GUILayoutShapeSize();
                    linePoints = 6;
                    closeLineLoop = true;
                    break;
                case LinePointsShape.Heptagon:
                    GUILayoutShapeSize();
                    linePoints = 7;
                    closeLineLoop = true;
                    break;
                case LinePointsShape.Octagon:
                    GUILayoutShapeSize();
                    linePoints = 8;
                    closeLineLoop = true;
                    break;
                case LinePointsShape.Nonagon:
                    GUILayoutShapeSize();
                    linePoints = 9;
                    closeLineLoop = true;
                    break;
                case LinePointsShape.Decagon:
                    GUILayoutShapeSize();
                    linePoints = 10;
                    closeLineLoop = true;
                    break;
                default:
                    break;
            }

            GUILayout.Space(8);

            alignment = (Alignment)EditorGUILayout.EnumPopup("Facing Axis", alignment);
            EditorGUI.indentLevel--;
        }

        void GUILayoutButton()
        {
            EditorGUILayout.LabelField("Create GameObject", EditorStyles.boldLabel);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            lineName = EditorGUILayout.TextField("Object Name", lineName);
            GUILayout.Space(12);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginDisabledGroup(lineColoring == LineColoring.Texture && lineMaterial == null);
            if (GUILayout.Button("Create Line", GUILayout.Width(120), GUILayout.Height(60)))
            {
                CreateAutoLine();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            GUILayout.Space(12);
            EditorGUILayout.HelpBox("DO NOT REARRANGE THE CHILD OBJECTS - DO NOT ADJUST THE ROTATION OF THE OBJECTS", MessageType.Warning);
        }

        public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        void GUILayoutShapeSize()
        {
            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            shapeSize = EditorGUILayout.Slider("Shape Size", shapeSize, 0.5f, 10);
            EditorGUI.indentLevel--;
        }

        private void CreateAutoLine()
        {
            //Create Parent GameObject
            GameObject lineContainerGO;
            lineContainerGO = new GameObject(lineName);

            //Add Component to Parent
            lineContainerGO.AddComponent<TBLineRendererComponent>();
            TBLineRenderer lineRenderer = new TBLineRenderer();
            TBLineRendererComponent lineRendererComponent = lineContainerGO.GetComponent<TBLineRendererComponent>();
            lineRendererComponent.lineRendererProperties = lineRenderer;

            //Assign properties to TBLineRenderer
            lineRenderer.lineWidth = lineWidth;
            lineRenderer.linePoints = linePoints;
            lineRenderer.roundedEndCaps = roundEndCaps;
            lineRenderer.roundedCorners = roundCorners;
            lineRenderer.closeLoop = closeLineLoop;
            lineRenderer.axis = (TBLineRenderer.Axis)alignment;
            lineRenderer.shape = (TBLineRenderer.Shape)linePointsShape;
            lineRenderer.shapeSize = shapeSize;

            switch (lineColoring)
            {
                case LineColoring.SolidColor:
                    lineRenderer.startColor = startColor;
                    lineRenderer.endColor = startColor;
                    break;
                case LineColoring.Gradient:
                    lineRenderer.startColor = startColor;
                    lineRenderer.endColor = endColor;
                    break;
                case LineColoring.Texture:
                    lineRenderer.texture = lineMaterial;

                    switch (lineMaterialEnum)
                    {
                        case LineMaterial.Stretch:
                            lineRenderer.textureMode = TBLineRenderer.TextureMode.Stretch;
                            break;
                        case LineMaterial.Tile:
                            lineRenderer.textureMode = TBLineRenderer.TextureMode.Tile;
                            break;
                        default:
                            break;
                    }

                    lineRenderer.startColor = lineRenderer.endColor = Color.white;
                    break;
                default:
                    break;
            }

            //Add child Objects and Gizmos
            GameObject pointsGO;
            Transform gizmoTarget = lineContainerGO.transform;
            for (int i = 1; i <= linePoints; i++)
            {
                if (i == linePoints)
                {
                    pointsGO = new GameObject("Ending Point");
                    pointsGO.AddComponent<TBLineRendererDrawGizmo>();
                    pointsGO.GetComponent<TBLineRendererDrawGizmo>().targetPoint = gizmoTarget;
                }
                else
                {
                    pointsGO = new GameObject("Point " + i);
                    pointsGO.AddComponent<TBLineRendererDrawGizmo>();
                    if (i != 1)
                        pointsGO.GetComponent<TBLineRendererDrawGizmo>().targetPoint = gizmoTarget;
                    gizmoTarget = pointsGO.transform;
                }
                pointsGO.transform.SetParent(lineContainerGO.transform);
                pointsGO.GetComponent<TBLineRendererDrawGizmo>().parent = lineContainerGO.GetComponent<TBLineRendererComponent>();
                pointsGO.GetComponent<TBLineRendererDrawGizmo>().hideFlags = HideFlags.HideInInspector;
            }

            lineRendererComponent.SetLineRendererProperties();
            lineRendererComponent.DrawBasicShape();

            //Automatically select new GameObject
            Selection.activeGameObject = lineContainerGO;
        }
    }
}
