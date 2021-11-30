namespace Roguelike.Engine.Map
{
    abstract class Map
    {
        public int Height { get; }
        public int Width { get; }
        
        protected Map(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public abstract ObjectOnMap GetObjWithCoord(int x, int y);
        public abstract void SetObjWithCoord(int x, int y, ObjectOnMap obj);
    }
}
