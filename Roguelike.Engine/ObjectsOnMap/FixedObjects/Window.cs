namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    class Window : FixedObject
    {
        public Window() : base()
        {
            Character = 'I';
            Description = "Window: hello, world!";
            Seethrough = true;
        }
    }
}
