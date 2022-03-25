using Roguelike.Engine.ObjectsOnMap;
namespace Roguelike.Engine.Maps
{
    public class MapCell
    {
        public ObjectOnMap[] Layers = new ObjectOnMap[4];
        public MapCell() 
        {
            Layers = new ObjectOnMap[4];
        }
    }
}
