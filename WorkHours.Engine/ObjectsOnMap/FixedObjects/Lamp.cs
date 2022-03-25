using Roguelike.GameConfig;
using System;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Lamp : FixedObject, ILuminous, IUsable
    {
        protected bool HasBulb;

        public int LightedAreaRadius { get; private set; }

        public bool Enabled { get; private set; }

        public Lamp(bool Enabled = true, ConsoleColor FG = ConsoleColor.Yellow) : base()
        {
            Character = 'o';
            this.ForegroundColor = FG;
            Description = "Lamp: Bright power saving lamp. ";
            MapLayer = Enums.MapLayer.CEILING;
            Walkable = true;
            Seethrough = true;
            HasBulb = true;
            LightedAreaRadius = 10;
            this.Enabled = Enabled;
        }

        public UseCallBack TryUse(object input)
        {
            if (!(input is InventoryObjects.LightBulb))
            {
                return new UseCallBack(false, false);
            }

            if (!Enabled)
            {
                GameLog.Add(LogMessages.LampEnabled);
                ForegroundColor = ConsoleColor.Yellow;
                Enabled = true;
            }

            return new UseCallBack(true, true);
        }
    }
}
