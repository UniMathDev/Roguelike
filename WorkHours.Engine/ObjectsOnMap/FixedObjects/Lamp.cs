using Roguelike.GameConfig;
using System;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Lamp : FixedObject, ILuminous, IUsable
    {
        public int LightedAreaRadius { get; private set; }

        public bool Enabled { get; set; }
        private bool hasBulb;
        private bool HasBulb
        {
            get
            {
                return hasBulb;
            }
            set
            {
                hasBulb = value;
                if (hasBulb)
                {
                    Description = "Lamp: Bright power saving lamp. ";
                }
                else
                {
                    Description = "Lamp: The bulb inside this one is broken. I could replace it. ";
                }
            }
        }


        public Lamp(bool Enabled = true, ConsoleColor FG = ConsoleColor.Yellow) : base()
        {
            Character = 'o';
            this.ForegroundColor = FG;
            MapLayer = Enums.MapLayer.CEILING;
            Walkable = true;
            Seethrough = true;
            LightedAreaRadius = 10;
            this.Enabled = Enabled;
            this.HasBulb = Enabled;
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
                HasBulb = true;
            }

            return new UseCallBack(true, true);
        }
    }
}
