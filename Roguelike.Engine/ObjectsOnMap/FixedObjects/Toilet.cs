namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Toilet : FixedObject
    {
        protected Toilet(char character) : base()
        {
            Character = character;
            Description = "Toilet: do you want to drink?";
            Seethrough = false;
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
