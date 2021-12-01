namespace Roguelike.Engine.ObjectsOnMap
{
    abstract class ObjectOnMap
    {
        public char Сharacter { get; }

        protected ObjectOnMap(char сharacter)
        {
            Сharacter = сharacter;
        }
    }
}
