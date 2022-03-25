using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.Enums;
using System.Drawing;
using System;

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

        public bool WithinBounds(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }
            if (x >= Width|| y >= Height)
            {
                return false;
            }
            return true;
        }

        public bool InPlayerFOV(int x, int y)
        {
            return WithinBounds(x, y) && GetTopObjWithCoord(x, y).InFOV;
        }

        public bool InIlluminatedArea(int x, int y, int illuminatedAreaRadius, Point lightSourceCoords)
        {
            if ((Math.Pow(x - lightSourceCoords.X, 2) / 4 +
                            Math.Pow(y - lightSourceCoords.Y, 2)) < Math.Pow(illuminatedAreaRadius, 2))
            {
                bool inIlluminatedArea = true;

                if (lightSourceCoords.X == x && lightSourceCoords.Y == y)
                    return true;

                int coordY;

                if (lightSourceCoords.X == x)
                {
                    int coordX = x;
                    int offset = (lightSourceCoords.Y < y ? 1 : -1);
                    for (coordY = lightSourceCoords.Y + offset; coordY != y; coordY += offset)
                    {
                        if (!GetTopObjWithCoord(coordX, coordY).Seethrough)
                        {
                            inIlluminatedArea = false;
                            break;
                        }
                    }

                    return inIlluminatedArea;
                }


                //y = kx + b
                double k = ((double)lightSourceCoords.Y - y) / ((double)lightSourceCoords.X - x);
                double b1 = 0;
                double b2 = 0;

                if (k < 0)
                {
                    b1 = (double)y + 0.1 - k * ((double)x + 0.1);
                    b2 = (double)y + 0.9 - k * ((double)x + 0.9);
                }
                else
                {
                    b1 = (double)y + 0.9 - k * ((double)x + 0.1);
                    b2 = (double)y + 0.1 - k * ((double)x + 0.9);
                }

                double offsetX = (lightSourceCoords.X < x ? 0.1 : -0.1);

                for (double coordX = lightSourceCoords.X + 0.1;
                    (int)Math.Floor(coordX) != x || (int)Math.Floor(k * coordX + b1) != y; coordX += offsetX)
                {
                    coordY = (int)Math.Floor(k * coordX + b1);
                    if (!GetTopObjWithCoord((int)Math.Floor(coordX), coordY)
                        .Seethrough)
                    {
                        inIlluminatedArea = false;
                        break;
                    }
                }

                if (inIlluminatedArea == true)
                    return inIlluminatedArea;

                inIlluminatedArea = true;

                for (double coordX = lightSourceCoords.X + 0.9;
                    (int)Math.Floor(coordX) != x || (int)Math.Floor(k * coordX + b2) != y; coordX += offsetX)
                {
                    coordY = (int)Math.Floor(k * coordX + b2);
                    if (!GetTopObjWithCoord((int)Math.Floor(coordX), coordY)
                        .Seethrough)
                    {
                        inIlluminatedArea = false;
                        break;
                    }
                }

                return inIlluminatedArea;
            }

            return false;
        }
    }
}
