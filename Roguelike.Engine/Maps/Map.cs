using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.Enums;

namespace Roguelike.Engine.Maps
{
    public class Map
    {
        public int Height { get; }
        public int Width { get; }

        private MapCell[,] _mapCells;

        public const int MAPCELL_LAYER_COUNT = 4;
        public Map(int height, int width, MapCell[,] objectsOnMap)
        {
            Height = height;
            Width = width;
            _mapCells = objectsOnMap;
        }

        public ObjectOnMap GetTopObjWithCoord(int x, int y)
        {
            for (int i = 0; i < MAPCELL_LAYER_COUNT; i++)
            {
                ObjectOnMap obj = _mapCells[y, x].Layers[i];
                if (obj != null && obj.Visible)
                {
                    return obj;
                }
            }
            throw new System.Exception("No visible objects found in MapCell.");
        }
        public ObjectOnMap GetObjWithCoord(int x, int y, MapLayer layer)
        {
            return _mapCells[y, x].Layers[(int)layer];
        }

        public void SetObjWithCoord(int x, int y, ObjectOnMap obj)
        {
            _mapCells[y, x].Layers[(int)obj.MapLayer] = obj;
        }
        public void SetObjWithCoordToNull(int x, int y, MapLayer layer)
        {
            _mapCells[y, x].Layers[(int)layer] = null;
        }

        public char GetCharWithCoord(int x, int y)
        {
            return GetTopObjWithCoord(x, y).Character;
        }

        public bool IsPossibleToMove(int x, int y)
        {
            for (int i = 0; i < MAPCELL_LAYER_COUNT; i++)
            {
                if(_mapCells[y,x].Layers[i] != null && !(_mapCells[y,x].Layers[i].Walkable))
                {
                    return false;
                }
            }
            return true;
        }
        //GetMapInStringArray()
        /*
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
        */
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
    }
}
