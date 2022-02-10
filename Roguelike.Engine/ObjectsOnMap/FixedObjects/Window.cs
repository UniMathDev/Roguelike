namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Window : FixedObject
    {
        public Window()
        {
            Character = 'I';
            Description = "Window: hello, world!";
            Seethrough = true;
        }
    }
}
