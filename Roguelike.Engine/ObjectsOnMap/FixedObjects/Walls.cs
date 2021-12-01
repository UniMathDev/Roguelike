namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    abstract class Wall : FixedObject
    {
        protected Wall(char сharacter) : base(сharacter)
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
