using System.Diagnostics;
namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Door : VariableFixedObject
    {
        private char openedChar = ' ';

        private char closedChar;

        public bool isOpen = false;
        public Door()
        {
            Character = '/';
            Description = "Door: a white office door. ";
            Seethrough = false;
            closedChar = Character;
        }
        
        public override void Use(object input)
        {
            if (isOpen)
            {
                Character = closedChar;
                isOpen = false;
            }
            else
            {
                Character = openedChar;
                isOpen = true;
            }
            Debug.WriteLine("1");
        }
        
    }

    class KeyClosedDoor : Door
    {
        public KeyClosedDoor()
        {
            Character = '/';
            Description = "Door: find a key or another way to open it.";
            Seethrough = false;
        }
    }
}
