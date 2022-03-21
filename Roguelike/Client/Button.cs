using System;

namespace Roguelike.Client
{
    public class Button
    {
        public int X;
        public int Y;
        public int width;
        public int height;
        public Action OnLeftPress;
        public Action OnRightPress;
        public Button(int X, int Y, int width, int height, Action OnLeftPress, Action OnRightPress)
        {
            this.X = X;
            this.Y = Y;
            this.width = width;
            this.height = height;
            this.OnLeftPress += OnLeftPress.Invoke;
            this.OnRightPress += OnRightPress.Invoke;
        }
        public bool TryPress(int X, int Y, bool isLeftClick)
        {
            if (this.X <= X && this.X + width > X && this.Y <= Y && this.Y + height > Y)
            {
                if (isLeftClick)
                    OnLeftPress.Invoke();
                else
                    OnRightPress.Invoke();
                return true;
            }
            return false;
        }
    }
}
