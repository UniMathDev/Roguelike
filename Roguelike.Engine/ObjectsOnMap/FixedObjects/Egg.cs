
namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Egg : FixedObject
    {
        public int Timer;

        public int X;

        public int Y;
        public Egg() : base('o', "///placeholder///")
        {
            Timer = 4;
        }
    }
}
