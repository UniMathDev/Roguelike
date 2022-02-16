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

            _fixedObjects.Add('h', new Chair());
            _fixedObjects.Add('g', new Deadbody());
            _fixedObjects.Add('X', new KeyClosedDoor());
            _fixedObjects.Add('N', new Fridge());
            _fixedObjects.Add('Y', new Hanger());
            _fixedObjects.Add('D', new Screen());
            _fixedObjects.Add('╒', new LeftTableLeg());
            _fixedObjects.Add('═', new CenterOfTable());
            _fixedObjects.Add('╕', new RightTableLeg());
            _fixedObjects.Add('B', new FirstPartOfToilet());
            _fixedObjects.Add('O', new SecondPartOfToilet());
            _fixedObjects.Add('█', new Wardrobe());
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
