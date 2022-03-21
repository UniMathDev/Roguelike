using Roguelike.GameConfig;

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
        
        public UseCallBack TryUse(object input)
        {
            if (input != null)
            {
                return new UseCallBack(false,false);
            }

            if (Walkable)
            {
                GameLog.Add(LogMessages.DoorClosed);
                Character = closedChar;
                Walkable = false;
            }
            else
            {
                GameLog.Add(LogMessages.DoorOpened);
                Character = openedChar;
                Walkable = true;
            }

            return new UseCallBack(true, false); ;
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
