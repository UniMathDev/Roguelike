namespace Roguelike.Engine.ObjectsOnMap
{
    public interface IUsable : IChangeAble
    {
        public bool TryUse(object useWith);
    }
    public struct UseCallBack
    {
        public readonly bool Success;
        public readonly bool ItemUsedUp;
        public UseCallBack(bool success, bool itemUsedUp)
        {
            Success = success;
            ItemUsedUp = itemUsedUp;
        }
    }
}
