﻿namespace Roguelike.Engine.ObjectsOnMap
{
    public interface IUsable : IChangeAble
    {
        public UseCallBack TryUse(object useWith)
        {
            return new UseCallBack(false, false);
        }
    }
    public struct UseCallBack
    {
        public bool Success;
        public bool ItemUsedUp;
        public UseCallBack(bool success, bool itemUsedUp)
        {
            Success = success;
            ItemUsedUp = itemUsedUp;
        }
    }
}
