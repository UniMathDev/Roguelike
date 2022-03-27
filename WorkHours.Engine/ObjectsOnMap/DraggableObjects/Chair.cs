namespace Roguelike.Engine.ObjectsOnMap.DraggableObjects
{
    class Chair : MobileObject
    {
        public Chair() : base()
        {
            Character = 'h';
            Description = "Chair: despite it being work hours," +
                " I think it's no time for sitting around. ";
            Seethrough = true;
            Draggable = true;
        }
        public Chair(int X, int Y) : this()
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
