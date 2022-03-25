namespace Roguelike.Engine.InventoryObjects
{
    public class InventoryObject
    {
        public int Size { get; protected set; }
        public string Description { get; protected set; }
        public bool TwoHanded { get; protected set; } = false;
    }
}
