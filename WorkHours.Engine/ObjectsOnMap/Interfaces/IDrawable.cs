using System;

namespace Roguelike.Engine.ObjectsOnMap
{
    public interface IDrawable
    {
        char Character { get; }
        ConsoleColor ForegroundColor { get; }
        ConsoleColor? CurrentForegroundColor { get; }
        ConsoleColor BackgroundColor { get; }
        public void Write() {
            if (ForegroundColor != ConsoleColor.White)
            {
                Console.BackgroundColor = BackgroundColor;
                Console.ForegroundColor = ForegroundColor;
            }
            else
            {
                Console.BackgroundColor = BackgroundColor;
                Console.ForegroundColor = (ConsoleColor) CurrentForegroundColor;
            }
            Console.Write(Character);
            Console.ResetColor();
        }
    }
}
