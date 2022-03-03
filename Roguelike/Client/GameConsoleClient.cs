using Roguelike.Engine;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.GameConfig;
using Roguelike.Input;
using Roguelike.Engine.ObjectsOnMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using Roguelike.GameConfig.GUIElements;

namespace Roguelike.Client
{
    class GameConsoleClient
    {
        private readonly Dictionary<ConsoleKey, GameMenuItem> _keyboardMenu;
        private readonly Dictionary<ConsoleKey, Direction> _directionKeys;
        private readonly Game _game;
        private readonly GUI _GUI;

        private bool interceptNextInput;

        public GameConsoleClient()
        {
            Map map = TxtToMapConverter.ConvertToArrayMap(@"..\..\..\..\Maps\Maps.txt", MapSize.Height, MapSize.Width);
            Player player = new(PlayerInitCoords.X, PlayerInitCoords.Y);
            map.SetObjWithCoord(PlayerInitCoords.X, PlayerInitCoords.Y, player);
            _game = new(map, player);
            _GUI = new(_game);

            InputManager.Start();
            InputManager.KeyPress += OnKeyPress;
            _keyboardMenu = new Dictionary<ConsoleKey, GameMenuItem>();
            _keyboardMenu.Add(ConsoleKey.Escape, new GameMenuItem(Exit));
            _keyboardMenu.Add(ConsoleKey.NumPad0, new GameMenuItem(Wait));

            #region _directionKeys assignment
            _directionKeys = new Dictionary<ConsoleKey, Direction>();
            _directionKeys.Add(ConsoleKey.UpArrow, Direction.Up);
            _directionKeys.Add(ConsoleKey.DownArrow, Direction.Down);
            _directionKeys.Add(ConsoleKey.LeftArrow, Direction.Left);
            _directionKeys.Add(ConsoleKey.RightArrow, Direction.Right);

            _directionKeys.Add(ConsoleKey.NumPad8, Direction.Up);
            _directionKeys.Add(ConsoleKey.NumPad9, Direction.RightUp);
            _directionKeys.Add(ConsoleKey.NumPad6, Direction.Right);
            _directionKeys.Add(ConsoleKey.NumPad3, Direction.RightDown);
            _directionKeys.Add(ConsoleKey.NumPad2, Direction.Down);
            _directionKeys.Add(ConsoleKey.NumPad1, Direction.LeftDown);
            _directionKeys.Add(ConsoleKey.NumPad4, Direction.Left);
            _directionKeys.Add(ConsoleKey.NumPad7, Direction.LeftUp);
            #endregion

            InputManager.RMousePress += OnRightClick;
            InputManager.LMousePress += OnLeftClick;
            InputManager.MouseMoved += OnMouseMove;
            OnInputIntercept += ClearIntercept;
        }

        public void Start()
        {
            _GUI.PrintGame();
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

            if (_directionKeys.ContainsKey(k.key))
            {
                Move(_directionKeys[k.key]);
            }

        }

        private bool drewGroundItemListLastMouseMove = false;
        void OnMouseMove(MOUSE_MOVE_INFO m)
        {
            if (interceptNextInput)
            {
                return;
            }
            if (_GUI.PrintGroundItemList(m.X, m.Y) && !drewGroundItemListLastMouseMove)
            {
                drewGroundItemListLastMouseMove = true;
            }
            else if(!_GUI.PrintGroundItemList(m.X, m.Y) && drewGroundItemListLastMouseMove)
            {
                drewGroundItemListLastMouseMove = false;
                _GUI.PrintGame();
            }
        }
        private void Move(Direction direction)
        {
            _game.Move(direction);
            _GUI.PrintGame();
        }

        private void Wait()
        {
            _game.Wait();
            _GUI.PrintGame();
        }

        private void OnLeftClick(MOUSE_PRESS_INFO m)
        {
            if (interceptNextInput)
            {
                interceptNextInput = false;
                OnInputIntercept.Invoke();
                return;
            }
            if (TryClickInterfaceButtons(m.X,m.Y))
            {
                _GUI.PrintGame();
                return;
            }
            if (_game._map.WithinBounds(m.X, m.Y))
            {
                Point OnMap = _GUI.BufferToMapPos(m.X, m.Y);
                _game.Interact(OnMap.X, OnMap.Y, null);
                _GUI.PrintGame();
                return;
            }
        }



        private void OnRightClick(MOUSE_PRESS_INFO m)
        {
            if (interceptNextInput)
            {
                interceptNextInput = false;
                OnInputIntercept.Invoke();
                return;
            }
            _GUI.PrintCellDescription(m.X, m.Y);
            interceptNextInput = true;
            OnInputIntercept += _GUI.PrintGame;
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

        private bool TryClickInterfaceButtons(int X, int Y)
        {
            //RevealCeilingButton
            if (X >= RevealCeilingButton.X && X <= RevealCeilingButton.X + RevealCeilingButton.width 
                && Y >= RevealCeilingButton.Y && Y <= RevealCeilingButton.Y + RevealCeilingButton.height)
            {
                _game._map.ShowCeiling = !_game._map.ShowCeiling;
                return true;
            }
            /*
            if ()
            {

                return;
            }
            */
            return false;
        }

        private void Exit()
        {
            Console.Clear();
            Environment.Exit(0);
        }
    }
}