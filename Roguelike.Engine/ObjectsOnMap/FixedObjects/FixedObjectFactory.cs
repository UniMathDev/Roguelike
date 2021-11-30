using Roguelike.Engine.Map.ObjectsOnMap;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class FixedObjectFactory
    {
        private Dictionary<char, FixedObject> _fixedObjects = new();
        public FixedObjectFactory()
        {
            _fixedObjects.Add('/', new Door());
            _fixedObjects.Add('.', new Floor());
            _fixedObjects.Add('|', new VerticalWall());
            _fixedObjects.Add('_', new HorizontalWall());
            _fixedObjects.Add('I', new Window());
        }
        public FixedObject GetFixedObject(char character)
        {
            /*if (!_fixedObjects.ContainsKey(character))
                throw ;*/
            return _fixedObjects[character] as FixedObject;
        }
    }
}
