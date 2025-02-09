﻿using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Toilet : FixedObject
    {
        protected Toilet(char character) : base()
        {
            Character = character;
            Description = "Toilet: I'm not THAT thirsty for now. ";
            Seethrough = false;
        }
    }
    class FirstPartOfToilet : Toilet
    {
        public FirstPartOfToilet() : base('B')
        {
        }
    }
    class SecondPartOfToilet : Toilet, ISearchable
    {
        public bool WasSearched { get; private set; }

        public List<InventoryObject> Inventory { get; set; } = new();

        public SecondPartOfToilet() : base('O')
        {
        }
    }
}
