using System.Diagnostics;
namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Door : FixedObject , IUsable
    {
        private char openedChar = ' ';

        private char closedChar;
        public Door() : base()
        {
            Character = '/';
            Description = "Door: a white office door. ";
            Seethrough = false;
            Walkable = false;
            closedChar = Character;
            MapLayer = Enums.MapLayer.SECONDARY;
        }
        
        public void Use(object input)
        {
            if (Walkable)
            {
                Character = closedChar;
                Walkable = false;
            }
            else
            {
                Character = openedChar;
                Walkable = true;
            }
        }
        
    }

    class KeyClosedDoor : Door
    {
        public KeyClosedDoor() : base()
        {
            Description = "Door: find a key or another way to open it.";
        }
    }
}
