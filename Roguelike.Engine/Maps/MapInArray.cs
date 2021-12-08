using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;

namespace Roguelike.Engine.Maps
{
    class MapInArray : Map
    {
        private ObjectOnMap[,] _objectsOnMap;

        public MapInArray(int height, int width, ObjectOnMap[,] objectsOnMap) : base(height, width)
        {
            _objectsOnMap = objectsOnMap;
        }

        public override ObjectOnMap GetObjWithCoord(int x, int y)
        {
            return _objectsOnMap[y, x];
        }

        public override void SetObjWithCoord(int x, int y, ObjectOnMap obj)
        {
            _objectsOnMap[y, x] = obj;
        }

        public override char GetCharWithCoord(int x, int y)
        {
            return _objectsOnMap[y, x].Сharacter;
        }

        public override bool IsPossibleToMove(int x, int y)
        {
            return _objectsOnMap[y, x] is Floor;
        }
    }
}
