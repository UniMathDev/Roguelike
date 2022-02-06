namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Wall : FixedObject
    {
        protected Wall(char сharacter) : base(сharacter, "Wall: a boring white wall. I guess you can spit on it for entertainment.")
        {
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
