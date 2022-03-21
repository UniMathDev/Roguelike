using System;

namespace Roguelike.Engine.ObjectsOnMap
{
    public interface IDrawable
    {
        char Character { get; }
        ConsoleColor ForegroundColor { get; }
        ConsoleColor BackgroundColor { get; }
        public void Write() {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.Write(Character);
            Console.ResetColor();
        }
    }
}
