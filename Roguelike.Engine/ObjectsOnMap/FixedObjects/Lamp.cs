using Roguelike.GameConfig;
using System;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Lamp : FixedObject, ILuminous, IUsable
    {
        protected bool HasBulb;

        public int LightedAreaRadius { get; private set; }

        public bool Enabled { get; private set; }

        public Lamp() : base()
        {
            Character = 'o';
            ForegroundColor = ConsoleColor.White;
            Description = "Lamp: Bright power saving lamp. ";
            MapLayer = Enums.MapLayer.CEILING;
            Walkable = true;
            Seethrough = true;
            HasBulb = true;
            LightedAreaRadius = 10;
            Enabled = false;
        }

        public UseCallBack TryUse(object input)
        {
            if (input != null)
            {
                return new UseCallBack(false, false);
            }

            if (Enabled)
            {
                GameLog.Add(LogMessages.LampDisabled);
                ForegroundColor = ConsoleColor.White;
                Enabled = false;
            }
            else
            {
                GameLog.Add(LogMessages.LampEnabled);
                ForegroundColor = ConsoleColor.Yellow;
                Enabled = true;
            }

            return new UseCallBack(true, false); ;
        }
    }
}
