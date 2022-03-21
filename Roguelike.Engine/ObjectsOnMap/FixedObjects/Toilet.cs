namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Toilet : FixedObject
    {
        protected Toilet(char character) : base()
        {
            Character = character;
            Description = "Toilet: I'm not THAT thirsty for now. ";
        }
    }
    class FirstPartOfToilet : Toilet
    {
        public FirstPartOfToilet() : base('B')
        {
        }
    }
    class SecondPartOfToilet : Toilet
    {
        public SecondPartOfToilet() : base('O')
        {
        }
    }
}
