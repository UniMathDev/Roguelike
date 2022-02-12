namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class ObjectOnMap
    {
        public char Character { get; protected set; }
        public string Description { get; protected set; }
        public bool Seethrough { get; protected set; }
        
        public void SetDisplayedCharacter(char character)
        {
            Character = character;
        }

        public virtual void Use(object input)
        {
            //do nothing
        }
    }
}
