using Roguelike.Engine.ObjectsOnMap;
using System.Text;

namespace Roguelike.Engine.Maps
{
    public abstract class Map
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

        public abstract char GetCharWithCoord(int x, int y);

        public abstract bool IsPossibleToMove(int x, int y);

        public override string ToString()
        {
            StringBuilder mapInString = new();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    mapInString.Append(this.GetCharWithCoord(x, y));
                }
                mapInString.Append('\n');
            }
            return mapInString.ToString();
        }
    }
}
