﻿using System;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Lamp : FixedObject, IUsable
    {
        protected bool HasBulb;

        public Lamp() : base()
        {
            Character = 'o';
            ForegroundColor = ConsoleColor.Yellow;
            Description = "Lamp: Bright power saving lamp. ";
            MapLayer = Enums.MapLayer.CEILING;
            Walkable = true;
        }
        public UseCallBack TryUse(object obj)
        {
            //if a lamp was given then put HasBulb to true;
            return new UseCallBack();
        }
    }
}
