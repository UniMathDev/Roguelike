namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Door : FixedObject
    {
        private char opened = ' ';
        public Door() : base('/', "Door: a white office door. ")
        {
        }
    }
}
