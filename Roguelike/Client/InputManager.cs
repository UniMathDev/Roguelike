using System;
using ConsoleLib;
using static ConsoleLib.NativeMethods;
namespace MouseInput
{
    public static class InputManager
    {
        public static event MousePressEvent RMousePress;
        public static event MousePressEvent LMousePress;
        public static event KeyPressEvent KeyPress;


        public delegate void MousePressEvent(MOUSE_PRESS_INFO m);
        public delegate void KeyPressEvent(KEY_PRESS_INFO k);

        static bool singleRMBClick = true;
        static bool singleLMBClick = true;

        public static void Start()
        {
            IntPtr inHandle = GetStdHandle(STD_INPUT_HANDLE);
            uint mode = 0;
            GetConsoleMode(inHandle, ref mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE;
            mode |= ENABLE_WINDOW_INPUT;
            mode |= ENABLE_MOUSE_INPUT;
            SetConsoleMode(inHandle, mode);
            ConsoleListener.Start();
            ConsoleListener.MouseEvent += OnMouseEvent;
            ConsoleListener.KeyEvent += OnKeyboardEvent;

        }
        static void OnMouseEvent(MOUSE_EVENT_RECORD r)
        {
            MOUSE_PRESS_INFO pressInfo;

            pressInfo.X = r.dwMousePosition.X;
            pressInfo.Y = r.dwMousePosition.Y;

            EnvokeEventIfPressSingle(r);
        }

        static void OnKeyboardEvent(KEY_EVENT_RECORD r)
        {
            KEY_PRESS_INFO pressInfo;
            pressInfo.key = (ConsoleKey)r.wVirtualKeyCode;

            if (r.bKeyDown)
                if (KeyPress != null)
                    KeyPress.Invoke(pressInfo);
        }

        // В ConsoleLib если зажать кнопку мыши и потянуть это будет регистрироваться как куча нажатий этой кнопки,
        // чтобы подобное фиксировалось как единственное нажатие нужна эта функция.
        static void EnvokeEventIfPressSingle(MOUSE_EVENT_RECORD r)
        {
            MOUSE_PRESS_INFO info = new MOUSE_PRESS_INFO(r.dwMousePosition.X, r.dwMousePosition.Y);

            //   RMB
            if (r.dwButtonState == MOUSE_EVENT_RECORD.RIGHTMOST_BUTTON_PRESSED && singleRMBClick)
            {
                if (RMousePress != null)
                    RMousePress.Invoke(info);
                singleRMBClick = false;
            }
            if (r.dwButtonState != MOUSE_EVENT_RECORD.RIGHTMOST_BUTTON_PRESSED)
            {
                singleRMBClick = true;
            }

            //   LMB
            if (r.dwButtonState == MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED && singleLMBClick)
            {
                if (LMousePress != null)
                    LMousePress.Invoke(info);
                singleLMBClick = false;
            }
            if (r.dwButtonState != MOUSE_EVENT_RECORD.FROM_LEFT_1ST_BUTTON_PRESSED)
            {
                singleLMBClick = true;
            }
        }
    }

    public struct MOUSE_PRESS_INFO
    {
        //x и у в символах, начинало координат в верхнем левом углу буфера консоли.
        public int X;
        public int Y;
        public MOUSE_PRESS_INFO(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public struct KEY_PRESS_INFO
    {
        public ConsoleKey key;
    }
}