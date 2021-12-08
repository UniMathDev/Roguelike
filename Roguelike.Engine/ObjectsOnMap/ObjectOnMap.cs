namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class ObjectOnMap
    {
        public char Сharacter { get; }

        protected ObjectOnMap(char сharacter)
        {
            Сharacter = сharacter;
        }
    }
}
