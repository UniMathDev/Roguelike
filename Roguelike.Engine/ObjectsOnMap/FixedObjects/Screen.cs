namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Screen : FixedObject
    {
        public Screen() : base()
        {
            Character = 'D';
            Description = "Screen: For showing boring presentations. ";
            Seethrough = false;
        }
    }
}
