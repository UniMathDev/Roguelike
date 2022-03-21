namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Fridge : FixedObject
    {
        public Fridge() : base()
        {
            Character = 'N';
            Description = "Fridge: I'm not hungry right now. ";
            Seethrough = false;
        }
    }
}
