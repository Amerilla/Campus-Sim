using UnityEditor;
using UnityEngine;

namespace TechnoBabelGames
{
    [CustomEditor(typeof(TBLineRendererComponent))]
    public class TBLineRendererComponentEditor : Editor
    {
        
        TBLineRendererComponent lineRendererComponent;
        TBLineRenderer lineRendererProperties;
        bool showShapeAdjustments = false;
        bool changeCloseLoop;
        float adjustedShapeSize;
        float adjustedLineWidth;
        Color changeStartColor;
        Color changeEndColor;
        Vector3[] m_HandlePosition;
        Vector3[] handlePositions { get { return m_HandlePosition; } set { m_HandlePosition = value; } }

        private void OnEnable()
        {
            lineRendererComponent = (TBLineRendererComponent)target;
            lineRendererProperties = lineRendererComponent.lineRendererProperties;
            lineRendererComponent.GetComponent<LineRenderer>().hideFlags = HideFlags.HideInInspector;
            adjustedShapeSize = lineRendererProperties.shapeSize;
            adjustedLineWidth = lineRendererProperties.lineWidth;
            changeCloseLoop = lineRendererProperties.closeLoop;
            changeStartColor = lineRendererProperties.startColor;
            changeEndColor = lineRendererProperties.endColor;
        }

        public override void OnInspectorGUI()
        {
            GUILayoutLineRendererPorperties();
            GUILayout.Space(8);
            TBLineRendererTool.DrawUILine(Color.gray);
            GUILayout.Space(8);
            GUILayoutColorProperties();
            GUILayout.Space(8);
            TBLineRendererTool.DrawUILine(Color.gray);
            GUILayout.Space(8);
            GUILayoutShapeAdjustments();
        }

        void GUILayoutLineRendererPorperties()
        {
            EditorGUILayout.LabelField("LineRenderer Properties", EditorStyles.boldLabel);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();

            float undoLineWidth = EditorGUILayout.Slider("Line Width", lineRendererProperties.lineWidth, 0.01f, 1);
            if(lineRendererProperties.lineWidth != adjustedLineWidth)
            {
                adjustedLineWidth = lineRendererProperties.lineWidth;
                lineRendererComponent.UpdateLineRendererLineWidth();
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(lineRendererComponent, "Changed Line Width");
                lineRendererProperties.lineWidth = undoLineWidth;
            }

            GUILayout.Space(8);

            EditorGUI.BeginChangeCheck();

            bool undoCloseLoop = EditorGUILayout.Toggle("Close Loop", lineRendererProperties.closeLoop);
            if(lineRendererProperties.closeLoop != changeCloseLoop)
            {
                changeCloseLoop = lineRendererProperties.closeLoop;
                lineRendererComponent.UpdateLineRendererLoop();
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(lineRendererComponent, "Changed Close Loop toggle");
                lineRendererProperties.closeLoop = undoCloseLoop;
            }

            EditorGUI.indentLevel--;
        }

        void GUILayoutColorProperties()
        {
            EditorGUILayout.LabelField("Color Properties", EditorStyles.boldLabel);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;

            if(lineRendererProperties.textureMode != TBLineRenderer.TextureMode.None)
            {
                EditorGUILayout.HelpBox("Use " + lineRendererProperties.texture.name + " (Material) to change the appearance of the line", MessageType.Info);
            }
            else
            {
                EditorGUI.BeginChangeCheck();

                Color undoStartColor = EditorGUILayout.ColorField("Start Color", lineRendererProperties.startColor);
                Color undoEndColor = EditorGUILayout.ColorField("End Color", lineRendererProperties.endColor);
                if (lineRendererProperties.startColor != changeStartColor || lineRendererProperties.endColor != changeEndColor)
                {
                    changeStartColor = lineRendererProperties.startColor;
                    changeEndColor = lineRendererProperties.endColor;
                    lineRendererComponent.UpdateLineRendererColor();
                }

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(lineRendererComponent, "Changed Line Color");
                    lineRendererProperties.startColor = undoStartColor;
                    lineRendererProperties.endColor = undoEndColor;
                }
            }
        }

        void GUILayoutShapeAdjustments()
        {
            EditorGUILayout.LabelField("Shape Properties", EditorStyles.boldLabel);

            GUILayout.Space(8);

            EditorGUI.indentLevel++;
            showShapeAdjustments = EditorGUILayout.Foldout(showShapeAdjustments, "Adjust Shape");
            if (showShapeAdjustments)
            {
                EditorGUI.indentLevel++;
                if (lineRendererProperties.shape == TBLineRenderer.Shape.None)
                {
                    EditorGUILayout.HelpBox("The size of custom shapes cannot be adjusted.", MessageType.Info);
                }
                else
                {
                    EditorGUILayout.HelpBox("Adjusting these settings will reset the position of the points!", MessageType.Warning);

                    GUILayout.Space(8);

                    EditorGUI.BeginChangeCheck();

                    float undoShapeSize = EditorGUILayout.Slider("Shape Size", lineRendererProperties.shapeSize, 0.1f, 50);
                    if (lineRendererProperties.shapeSize != adjustedShapeSize)
                    {
                        adjustedShapeSize = lineRendererProperties.shapeSize;
                        lineRendererComponent.DrawBasicShape();
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(lineRendererComponent, "Changed Shape Size");
                        lineRendererProperties.shapeSize = undoShapeSize;
                    }

                    GUILayout.Space(8);

                    EditorGUILayout.HelpBox("'Reset Shape' will set points back into the original shape but will NOT reset the shape size.", MessageType.Info);

                    GUILayout.Space(8);

                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Reset Shape", GUILayout.Width(100), GUILayout.Height(30)))
                    {
                        Undo.RecordObjects(lineRendererComponent.GetComponentsInChildren<Transform>(), "Reset Shape");
                        lineRendererComponent.DrawBasicShape();
                    }

                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();

                }
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
            }
        }

        void OnSceneGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                lineRendererComponent.SetPoints();
            }

            if(m_HandlePosition == null)
            {
                m_HandlePosition = new Vector3[lineRendererComponent.transform.childCount];
            }

            EditorGUI.BeginChangeCheck();
            Vector3[] newChildPositions = PlaceHandlesOnChildPositions();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(lineRendererComponent.GetComponentsInChildren<Transform>(), "Moved line point by handle");
                handlePositions = newChildPositions;
                MoveChildrenToHandlePositions();
            }            
        }

        Vector3[] PlaceHandlesOnChildPositions()
        {
            Vector3[] newVectors = new Vector3[lineRendererComponent.transform.childCount];

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.background = (Texture2D)EditorGUIUtility.IconContent("AvatarController.Layer").image;


            for (int i = 0; i < newVectors.Length; i++)
            {
                Handles.Label(
                    lineRendererComponent.transform.GetChild(i).position,
                    lineRendererComponent.transform.GetChild(i).name,
                    style
                );

                newVectors[i] = Handles.PositionHandle(
                    lineRendererComponent.transform.GetChild(i).position,
                    lineRendererComponent.transform.GetChild(i).rotation
                );
            }

            return newVectors;
        }

        void MoveChildrenToHandlePositions()
        {
            for (int i = 0; i < handlePositions.Length; i++)
            {
                lineRendererComponent.transform.GetChild(i).position = handlePositions[i];
            }
        }        

    }
}
