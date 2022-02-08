using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class FixedObjectFactory : Factory
    {
        private Dictionary<char, FixedObject> _fixedObjects = new();
        public FixedObjectFactory()
        {
            _fixedObjects.Add('/', new Door());
            _fixedObjects.Add('.', new Floor());
            _fixedObjects.Add('|', new VerticalWall());
            _fixedObjects.Add('_', new HorizontalWall());
            _fixedObjects.Add('I', new Window());
            _fixedObjects.Add('o', new Egg());
        }
        public override ObjectOnMap CreateObjectOnMap(char character)
        {
            return _fixedObjects[character] as ObjectOnMap;
        }
    }
}
