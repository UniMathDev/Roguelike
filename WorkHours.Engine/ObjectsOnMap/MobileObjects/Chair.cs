namespace Roguelike.Engine.ObjectsOnMap.MobileObjects
{
    class Chair : MobileObject
    {
        public Chair() : base()
        {
            Character = 'h';
            Description = "Chair: despite it being work hours," +
                " I think it's no time for sitting around right now. ";
            Seethrough = true;
        }
    }
}
