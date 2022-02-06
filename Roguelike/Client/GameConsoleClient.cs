using Roguelike.Engine;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.GameConfig;
using System;
using System.Collections.Generic;
using System.Threading;
using Input;
using ConsoleLib;

namespace Roguelike.Client
{
    class GameConsoleClient
    {
        private readonly Dictionary<ConsoleKey, GameMenuItem> _keyboardMenu;
        private readonly Game _game;
        private readonly GUI _GUI;

        private bool interceptNextInput;

        public GameConsoleClient()
        {
            Map map = TxtToMapConverter.ConvertToArrayMap(@"..\..\..\..\Maps\TestMap1.txt", MapSize.Height, MapSize.Width);
            Player player = new(PlayerInitCoords.X, PlayerInitCoords.Y);
            _game = new(map, player);
            _GUI = new(_game);

            InputManager.Start();
            InputManager.KeyPress += OnKeyPress;
            _keyboardMenu = new Dictionary<ConsoleKey, GameMenuItem>();
            _keyboardMenu.Add(ConsoleKey.Escape, new GameMenuItem(Exit));
            _keyboardMenu.Add(ConsoleKey.UpArrow, new GameMenuItem(MoveUp));
            _keyboardMenu.Add(ConsoleKey.DownArrow, new GameMenuItem(MoveDown));
            _keyboardMenu.Add(ConsoleKey.RightArrow, new GameMenuItem(MoveRight));
            _keyboardMenu.Add(ConsoleKey.LeftArrow, new GameMenuItem(MoveLeft));
            InputManager.RMousePress += Examine;
            InputManager.LMousePress += Use;

            OnInputIntercept += ClearIntercept;
        }

        public void Start()
        {
            _GUI.PrintAMove();
        }
        void OnKeyPress(KEY_PRESS_INFO k)
        {
            if (interceptNextInput)
            {
                interceptNextInput = false;
                OnInputIntercept.Invoke();
                return;
            }

            if (_keyboardMenu.ContainsKey(k.key))
            {
                _keyboardMenu[k.key].Action();
            }
        }

        private void MoveUp()
        {
            _game.Move(Directions.Up);
            _GUI.PrintAMove();
        }

        private void MoveDown()
        {
            _game.Move(Directions.Down);
            _GUI.PrintAMove();
        }

        private void MoveRight()
        {
            _game.Move(Directions.Right);
            _GUI.PrintAMove();
        }

        private void MoveLeft()
        {
            _game.Move(Directions.Left);
            _GUI.PrintAMove();
        }



        private void Use(MOUSE_PRESS_INFO m)
        {
            if (interceptNextInput)
            {
                interceptNextInput = false;
                OnInputIntercept.Invoke();
                return;
            }
            //_game.Use(m.X,m.Y);
            _GUI.PrintAMove();
        }
        private void Examine(MOUSE_PRESS_INFO m)
        {
            if (interceptNextInput)
            {
                interceptNextInput = false;
                OnInputIntercept.Invoke();
                return;
            }
            _GUI.PrintCellDescription(m.X, m.Y);
            interceptNextInput = true;
            OnInputIntercept += _GUI.PrintAMove;
        }

        /// <summary>
        /// Note: Удаляет все функции из списка подписанных после каждого вызова.
        /// </summary>
        private static event Action OnInputIntercept;
        private void ClearIntercept ()
        {
            if (OnInputIntercept != null)
                foreach (var d in OnInputIntercept.GetInvocationList())
                    OnInputIntercept -= (d as Action);
            OnInputIntercept += ClearIntercept;
        }


        private void Exit()
        {
            Console.Clear();
            Environment.Exit(0);
        }
    }
}
