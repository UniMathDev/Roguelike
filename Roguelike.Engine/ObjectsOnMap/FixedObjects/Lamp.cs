using System;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Lamp : FixedObject, ILuminous
    {
        protected bool HasBulb;
        public int LightedAreaRadius { get; private set; }

        public Lamp() : base()
        {
            Character = 'o';
            ForegroundColor = ConsoleColor.Yellow;
            Description = "Lamp: Bright power saving lamp. ";
            MapLayer = Enums.MapLayer.CEILING;
            Walkable = true;
            Seethrough = true;
            HasBulb = true;
            LightedAreaRadius = 10;
        }

    }
}
