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
        public bool ShowCeiling { get; set; } = false;
        public Map(int height, int width, MapCell[,] objectsOnMap)
        {
            Height = height;
            Width = width;
            _mapCells = objectsOnMap;
        }

        public ObjectOnMap GetTopObjWithCoord(int x, int y)
        {
            int startAt = 1;
            if (ShowCeiling)
            {
                startAt--;
            }
            for (int i = startAt; i < MAPCELL_LAYER_COUNT; i++)
            {
                ObjectOnMap obj = _mapCells[y, x].Layers[i];
                if (obj != null && !obj.Hidden)
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
