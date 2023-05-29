using UnityEngine;

namespace TechnoBabelGames
{
    [System.Serializable]
    public class TBLineRenderer
    {
        public int linePoints;
        public float lineWidth;
        public float shapeSize;
        public bool closeLoop;
        public bool roundedEndCaps;
        public bool roundedCorners;
        public Color startColor;
        public Color endColor;
        public Material texture;
        public TextureMode textureMode;
        public Shape shape;
        public Axis axis;
        public enum TextureMode { None, Stretch, Tile }
        public enum Shape { None, Line, Triangle, Square, Pentagon, Hexagon, Heptagon, Octagon, Nonagon, Decagon }
        public enum Axis { FaceCamera, X, Y, Z }
    }
}
