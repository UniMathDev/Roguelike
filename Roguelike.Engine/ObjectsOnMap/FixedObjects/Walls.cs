namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Wall : FixedObject
    {
        protected Wall(char character)
        {
            Character = character;
            Description = "Wall: a boring white wall. I guess you can spit on it for entertainment.";
            Seethrough = false;
        }
    }
    class VerticalWall : Wall
    {
        public VerticalWall() : base('|')
        {
        }
    }
    class HorizontalWall : Wall
    {
        public HorizontalWall() : base('_')
        {
        }
    }
}
