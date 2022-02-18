using Roguelike.Engine.Enums;
namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class FixedObject : ObjectOnMap
    {
        protected FixedObject() : base() {
            MapLayer = MapLayer.SPACE;
        }
    }
}
