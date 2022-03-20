namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Screen : FixedObject
    {
        public Screen() : base()
        {
            Character = 'D';
            Description = "Screen: information also can be a weapon.";
            Seethrough = false;
        }
    }
}
