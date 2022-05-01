namespace Asteroids.Utils
{
    public struct ScreenBounds
    {
        public float XMin;
        public float XMax;
        public float YMin;
        public float YMax;
        
        public float Width => XMax - XMin;
        public float Height => YMax - YMin;
    }
}