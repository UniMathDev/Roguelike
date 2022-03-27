using System.Collections.Generic;
using System;
using Roguelike.Engine.ObjectsOnMap.MobileObjects;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class ObjectOnMapFactory : Factory
    {
        private Dictionary<char, ObjectOnMap> _objectsOnMap = new();
        public ObjectOnMapFactory()
        {
            
            _objectsOnMap.Add('.', new Floor());
            _objectsOnMap.Add('|', new VerticalWall());
            _objectsOnMap.Add('_', new HorizontalWall());
            _objectsOnMap.Add('I', new Window());
            _objectsOnMap.Add('o', new Egg());
            _objectsOnMap.Add('/', new Door());

            _objectsOnMap.Add('h', new Chair());
            _objectsOnMap.Add('g', new Deadbody());
            _objectsOnMap.Add('X', new KeyClosedDoor());
            _objectsOnMap.Add('N', new Fridge());
            _objectsOnMap.Add('Y', new Hanger());
            _objectsOnMap.Add('D', new Screen());
            _objectsOnMap.Add('╒', new LeftTableLeg());
            _objectsOnMap.Add('═', new CenterOfTable());
            _objectsOnMap.Add('╕', new RightTableLeg());
            _objectsOnMap.Add('B', new FirstPartOfToilet());
            _objectsOnMap.Add('O', new SecondPartOfToilet());
            _objectsOnMap.Add('█', new Wardrobe());
        }
        public override ObjectOnMap CreateObjectOnMap(char character)
        {
            ObjectOnMap fixedObject = _objectsOnMap[character];
            return Activator.CreateInstance(fixedObject.GetType()) as ObjectOnMap;
        }
    }
}
