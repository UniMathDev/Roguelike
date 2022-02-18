using Roguelike.Engine.ObjectsOnMap;
using System.Text;
using System.Collections.Generic;
using Roguelike.Engine.Monsters;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;

namespace Roguelike.Engine.Maps
{
    public class Map
    {
        public int Height { get; }
        public int Width { get; }

        protected List<Egg> eggList;

        private ObjectOnMap[,] _objectsOnMap;

        public Map(int height, int width, ObjectOnMap[,] objectsOnMap, List<Egg> eggList)
        {
            Height = height;
            Width = width;
            _objectsOnMap = objectsOnMap;
            this.eggList = eggList;
        }

        public ObjectOnMap GetObjWithCoord(int x, int y)
        {
            return _objectsOnMap[y, x];
        }

        public void SetObjWithCoord(int x, int y, ObjectOnMap obj)
        {
            _objectsOnMap[y, x] = obj;
        }

        public char GetCharWithCoord(int x, int y)
        {
            return _objectsOnMap[y, x].Character;
        }

        public bool IsPossibleToMove(int x, int y)
        {
            return ((_objectsOnMap[y, x] is Floor) || ((_objectsOnMap[y, x] is Door && (_objectsOnMap[y, x] as Door).isOpen)));
        }

        public string[] ToStringArray(int xPosMap, int yPosMap, int width, int height)
        {
            var mapInStringArray = new string[Height];
            StringBuilder mapLine = new();
            int y0 = (yPosMap > 0 ? yPosMap : 0);
            y0 = (yPosMap > (Height - height) ? (Height - height) : y0);
            int x0 = (xPosMap > 0 ? xPosMap : 0);
            x0 = (xPosMap > (Width - width) ? (Width - width) : x0);
            for (int y = y0; y < (height + y0); y++)
            {
                for (int x = x0; x < (width + x0); x++)
                {
                    mapLine.Append(this.GetCharWithCoord(x, y));
                }
                mapInStringArray[y - y0] = mapLine.ToString();
                mapLine.Clear();
            }
            return mapInStringArray;
        }

        public bool WithinBounds(int X, int Y)
        {
            if (X < 0 || Y < 0)
            {
                return false;
            }
            if (X >= Width|| Y >= Height)
            {
                return false;
            }
            return true;
        }

        public List<Egg> GetEggList()
        {
            return eggList;
        }
    }
}
