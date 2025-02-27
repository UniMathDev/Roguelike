﻿using System;
using Roguelike.Engine.Enums;

namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class ObjectOnMap : IDrawable
    {
        public char Character { get; protected set; }
        public ConsoleColor ForegroundColor { get; protected set;} = ConsoleColor.White;
        public ConsoleColor? CurrentForegroundColor { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; protected set; } = ConsoleColor.Black;
        public string Description { get; protected set; }
        public MapLayer MapLayer { get; protected set; }
        public bool Seethrough { get; protected set; } = false;
        public bool Walkable { get; protected set; } = false;
        public bool Hidden { get; set; } = false;
        public bool InFOV { get; set; } = false;
        public bool LitUp { get; set; } = false;
    }
}
