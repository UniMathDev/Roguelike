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
        private readonly ButtonManager _buttonManager;
        private readonly Game _game;
        private readonly GUI _GUI;
        private ClickInfo lastClick;

        private bool interceptNextInput;

        #region Actions for UI buttons
        private Action emptyAction = () => { };

        #endregion
        public GameConsoleClient()
        {
            Map map = TxtToMapConverter.ConvertToArrayMap(@"..\..\..\..\Maps\Maps.txt", MapSize.Height, MapSize.Width);
            Player player = new(PlayerInitCoords.X, PlayerInitCoords.Y);
            map.SetObjWithCoord(PlayerInitCoords.X, PlayerInitCoords.Y, player);
            _game = new(map, player);
            _GUI = new(_game);
            _buttonManager = new();

            InputManager.Start();
            InputManager.KeyPress += OnKeyPress;
            _keyboardMenu = new Dictionary<ConsoleKey, GameMenuItem>();
            _keyboardMenu.Add(ConsoleKey.Escape, new GameMenuItem(Exit));
            _keyboardMenu.Add(ConsoleKey.NumPad0, new GameMenuItem(OnWaitButtonPress));
            _keyboardMenu.Add(ConsoleKey.D0, new GameMenuItem(OnWaitButtonPress));

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

            _game.player.inventory.InventoryUpdated += OnInventoryUpdate;

            #region static UI buttons creation
            //ceiling reveal button
            {
                Action leftClickAction = () =>
                {
                    _game.map.ShowCeiling = !_game.map.ShowCeiling;
                };
                int X = CeilingRevealButton.X;
                int Y = CeilingRevealButton.Y;
                int height = CeilingRevealButton.height;
                int width = CeilingRevealButton.width;
                
                _buttonManager.Add(new Button(X, Y, width,height, leftClickAction, emptyAction));
            }
            
            #endregion

            InputIntercepted += ClearIntercept;
        }

        public void Start()
        {
            _GUI.PrintStartScreen();
            interceptNextInput = true;
            InputIntercepted += Console.Clear;
            InputIntercepted += _GUI.PrintGame;
        }
        void OnKeyPress(KEY_PRESS_INFO k)
        {
            if (interceptNextInput)
            {
                interceptNextInput = false;
                InputIntercepted.Invoke();
                return;
            }

            if (_keyboardMenu.ContainsKey(k.key))
            {
                _keyboardMenu[k.key].Action();
            }

            if (_directionKeys.ContainsKey(k.key))
            {
                OnDirectionKeyPress(_directionKeys[k.key],k.altHeld);
            }

        }

        private bool drewGroundItemListLastMouseMove = false;
        void OnMouseMove(MOUSE_MOVE_INFO m)
        {
            if (interceptNextInput)
            {
                return;
            }

            //try print ground item list
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
        private void OnDirectionKeyPress(Direction direction, bool shiftHeld)
        {
            if (shiftHeld)
            {
                _game.Run(direction);
            }
            else
            {
                _game.Walk(direction);
            }
            _GUI.PrintGame();
        }

        private void OnWaitButtonPress()
        {
            _game.Wait();
            _GUI.PrintGame();
        }

        private void OnLeftClick(MOUSE_PRESS_INFO m)
        {
            lastClick = new(true, m.X, m.Y);
            if (interceptNextInput)
            {
                interceptNextInput = false;
                InputIntercepted.Invoke();
                return;
            }
            if (_buttonManager.TryClick(m.X,m.Y, true))
            {
                _GUI.PrintGame();
                return;
            }
            if (_GUI.BufferPosInsideDisplayArea(m.X,m.Y))
            {
                Point OnMap = _GUI.BufferToMapPos(m.X, m.Y);
                _game.Interact(OnMap.X, OnMap.Y);
                _GUI.PrintGame();
                return;
            }
        }

        private void OnRightClick(MOUSE_PRESS_INFO m)
        {
            lastClick = new(false, m.X, m.Y);
            if (interceptNextInput)
            {
                interceptNextInput = false;
                InputIntercepted.Invoke();
                return;
            }
            if (_buttonManager.TryClick(m.X, m.Y, false))
            {
                return;
            }
            if (_GUI.BufferPosInsideDisplayArea(m.X,m.Y))
            {
                _GUI.PrintCellDescription(m.X, m.Y);
                interceptNextInput = true;
                InputIntercepted += _GUI.PrintGame;
                return;
            }
        }

        private void OnInventoryUpdate()
        {
            _buttonManager.RemoveInventoryButtons();
            PlayerInventory inventory = _game.player.inventory;

            AddHandbuttons();

            AddPocketButtons();

            return;
            void AddHandbuttons()
            {
                for (int handIndex = 0; handIndex < 2; handIndex++)
                {
                    if (inventory.Hands[handIndex] != null)
                    {
                        IntWrapper indexWrapper = new(handIndex);
                        Action[] popupMenuActions =
                            {
                                //USE
                                () =>
                                {
                                    _game.TrySwitchActiveInventoryItem(indexWrapper.Value);
                                },
                                //DROP
                                () =>
                                {
                                    _game.DropItem(indexWrapper.Value, true);
                                },
                                //POCKET
                                () =>
                                {
                                    _game.PocketItem(indexWrapper.Value);
                                },
                            };
                        Action leftClickAction = () =>
                        {
                            _buttonManager.AddHandPopupMenu(indexWrapper.Value, popupMenuActions);
                            _GUI.PrintHandPopupMenu(indexWrapper.Value);
                            interceptNextInput = true;
                            InputIntercepted += TryClickPopupMenusWithLastClick;
                            InputIntercepted += _buttonManager.RemovePopupButtons;
                            InputIntercepted += _GUI.EraseInventoryPopups;
                            InputIntercepted += _GUI.PrintGame;
                        };
                        Action rightClickAction = () =>
                        {
                            interceptNextInput = true;
                            InputIntercepted += _GUI.PrintGame;
                            _GUI.PrintInventoryItemDescription(inventory.Hands[indexWrapper.Value]);
                        };
                        _buttonManager.AddHandInventoryButton(handIndex, rightClickAction, leftClickAction);
                    }
                }
            }
            void AddPocketButtons()
            {
                for (int i = 0; i < inventory.Pockets.Count; i++)
                {
                    IntWrapper indexWrapper = new(i);
                    Action[] popupMenuActions =
                            {
                                //GRAB
                                () =>
                                {
                                    _game.UnpocketItem(indexWrapper.Value);
                                },
                                //DROP
                                () =>
                                {
                                    _game.DropItem(indexWrapper.Value, false);
                                },
                            };
                    Action leftClickAction = () =>
                    {
                        _buttonManager.AddPocketPopupMenu(indexWrapper.Value, popupMenuActions);
                        _GUI.PrintPocketPopupMenu(indexWrapper.Value);
                        interceptNextInput = true;
                        InputIntercepted += TryClickPopupMenusWithLastClick;
                        InputIntercepted += _buttonManager.RemovePopupButtons;
                        InputIntercepted += _GUI.EraseInventoryPopups;
                        InputIntercepted += _GUI.PrintGame;
                    };
                    Action rightClickAction = () =>
                    {
                        interceptNextInput = true;
                        InputIntercepted += _GUI.PrintGame;
                        _GUI.PrintInventoryItemDescription(inventory.Pockets[indexWrapper.Value]);
                    };
                    _buttonManager.AddPocketInventoryButton(i, rightClickAction, leftClickAction);
                }
            }

        }

        /// <summary>
        /// Note: Удаляет все функции из списка подписанных после каждого вызова.
        /// </summary>
        private static event Action InputIntercepted;
        private void ClearIntercept ()
        {
            if (InputIntercepted != null)
                foreach (var d in InputIntercepted.GetInvocationList())
                    InputIntercepted -= (d as Action);
            InputIntercepted += ClearIntercept;
        }
        private void TryClickPopupMenusWithLastClick()
        {
            _buttonManager.TryClickPopups(lastClick.X, lastClick.Y, lastClick.isLeftCLick);
        }
        private void Exit()
        {
            Console.Clear();
            Environment.Exit(0);
        }
        public class ClickInfo
        {
            public readonly bool isLeftCLick;
            public readonly int X;
            public readonly int Y;
            public ClickInfo(bool isLeftCLick, int x, int y)
            {
                this.isLeftCLick = isLeftCLick;
                X = x;
                Y = y;
            }
        }

    }
}