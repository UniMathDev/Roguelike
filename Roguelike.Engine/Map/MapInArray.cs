namespace Roguelike.Engine.Map
{
    class MapInArray : Map
    {
        private IObjectOnMap[,] _objectsOnMap;

        public MapInArray(int width, int height, IObjectOnMap[,] objectsOnMap) : base(width, height)
        {
            _objectsOnMap = objectsOnMap;
        }

        public override IObjectOnMap GetObjWithCoord(int x, int y)
        {
            return _objectsOnMap[x, y];
        }

        public override void SetObjWithCoord(int x, int y, IObjectOnMap obj)
        {
            _objectsOnMap[x, y] = obj;
        }
    }
}
