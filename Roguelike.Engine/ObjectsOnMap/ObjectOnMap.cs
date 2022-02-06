namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class ObjectOnMap
    {
        public char Сharacter { get; }

        public string Description { get; }

        protected ObjectOnMap(char сharacter, string description)
        {
            Сharacter = сharacter;
            Description = description;
        }
    }
}
