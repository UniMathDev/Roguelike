using Roguelike.Engine.ObjectsOnMap;

namespace Roguelike.Engine.Map
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
            return _objectsOnMap[x, y];
        }

        public override void SetObjWithCoord(int x, int y, ObjectOnMap obj)
        {
            _objectsOnMap[x, y] = obj;
        }
    }
}
