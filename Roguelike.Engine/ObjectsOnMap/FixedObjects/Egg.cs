namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Egg : FixedObject, IChangeAble
    {
        public int Timer;

        public int X;

        public int Y;
        public Egg() : base()
        {
            Character = 'o';
            ForegroundColor = System.ConsoleColor.DarkMagenta;
            Description = "Egg: I think it's pulsating a little. ";
            Timer = GameMath.rand.Next(7,11);
            Seethrough = true;
        }
    }
}
