namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Fridge : FixedObject
    {
        public Fridge() : base()
        {
            Character = 'N';
            Description = "Fridge: hope is a good breakfast, but a bad supper.";
            Seethrough = false;
        }
    }
}
