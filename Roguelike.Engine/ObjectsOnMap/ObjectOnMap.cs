using System;

namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class ObjectOnMap : IDrawable
    {
        public char Character { get; protected set; }
        public ConsoleColor ForegroundColor { get; protected set;} = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; protected set; } = ConsoleColor.Black;
        public string Description { get; protected set; }
        public bool Seethrough { get; protected set; }
        
        public void SetDisplayedCharacter(char character)
        {
            Character = character;
        }
    }
}
