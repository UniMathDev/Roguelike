using System.Collections.Generic;
using System;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class FixedObjectFactory : Factory
    {
        private Dictionary<char, FixedObject> _fixedObjects = new();
        public FixedObjectFactory()
        {
            
            _fixedObjects.Add('.', new Floor());
            _fixedObjects.Add('|', new VerticalWall());
            _fixedObjects.Add('_', new HorizontalWall());
            _fixedObjects.Add('I', new Window());
            _fixedObjects.Add('o', new Egg());
            _fixedObjects.Add('/', new Door());
        }
        public override ObjectOnMap CreateObjectOnMap(char character)
        {
            FixedObject fixedObject = _fixedObjects[character];
            if (fixedObject is VariableFixedObject)
            {
                return Activator.CreateInstance(fixedObject.GetType()) as ObjectOnMap;
            }
            else
            {
                return _fixedObjects[character] as ObjectOnMap;
            }
        }
    }
}
