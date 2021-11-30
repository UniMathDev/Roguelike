namespace Roguelike.Engine.Map
{
    abstract class Map
    {
        public int Width { get; }
        public int Height { get; }

        protected Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public abstract IObjectOnMap GetObjWithCoord(int x, int y);
        public abstract void SetObjWithCoord(int x, int y, IObjectOnMap obj);
    }
}
