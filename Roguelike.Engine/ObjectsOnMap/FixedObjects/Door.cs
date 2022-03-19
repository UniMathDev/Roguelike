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
        
        public bool TryUse(object input)
        {
            if (input != null)
            {
                return false;
            }

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
            return true;
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
