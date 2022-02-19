namespace Roguelike.Engine.Enums
{
    public enum MapLayer 
    {
        CEILING = 0, //лампы
        SPACE = 1, //стены, мебель, монстры, игрок. Объекты на этом слою могут передвигаться.
        ITEM = 2, //брошенные на землю предметы
        FLOOR = 3 //пол
    }
}
