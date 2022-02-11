namespace Roguelike.Engine.ObjectsOnMap.FixedObjects
{
    public class Egg : VariableFixedObject
    {
        public int Timer;

        public int X;

        public int Y;
        public Egg()
        {
            Character = 'o';
            Description = "Egg: «Кроличьи Яйца» – «Rabbit’s Eggs». И нечего тут скалить зубы, ребята." +
            " Если бы Киплинг имел в виду то же, что и вы, он бы написал «Rabbit’s Balls». (с) Братья стругацкие ";
            Seethrough = true;
            Timer = GameMath.rand.Next(7,11);
        }
    }
}
