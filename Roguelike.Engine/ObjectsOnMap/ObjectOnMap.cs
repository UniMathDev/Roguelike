namespace Roguelike.Engine.Map
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
