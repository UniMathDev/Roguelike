﻿using Roguelike.Engine;
using Roguelike.Engine.Enums;
using Roguelike.Engine.InventoryObjects;
using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using Roguelike.GameConfig;
using Roguelike.GameConfig.GUIElements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Roguelike.Client
{
    class GUI
    {
        private readonly Game _game;
        private bool firstGamePrint = true;

        private Point CameraCenterOffset =
            new Point(MapDisplaySize.Width / 2, MapDisplaySize.Height / 2);

        public GUI(Game game)
        {
            _game = game;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetWindowSize(WindowSize.Width, WindowSize.Height);
        }

        public void PrintGame()
        {
            if (firstGamePrint)
            {
                EraseInventoryPopups();
                firstGamePrint = false;
            }
            PrintInventory();
            PrintMap();
            PrintRevealCeilingButton();
            PrintStatusBar(StatusBars.X, StatusBars.Y,
                StatusBars.Length, ConsoleColor.DarkRed, _game.player.Health, PlayerStats.MaxHealth);
            PrintStatusBar(StatusBars.X, StatusBars.Y + 1,
                StatusBars.Length, ConsoleColor.DarkGray, _game.player.Stamina, PlayerStats.MaxStamina);
            PrintLogBox();

            Console.SetCursorPosition(0, 0);

            Console.SetCursorPosition(0, 2);
            Console.Write("Turn number: " + _game.playerTurnNumber + " ");

            Console.CursorVisible = false;
        }
        private void PrintLogBox()
        {
            int X = LogBox.X;
            int Y = LogBox.Y;
            Console.SetCursorPosition(X,Y);
            Console.Write(LogBox.String[0]);
            for (int i = 0; i < GameLog.logLength; i++)
            {
                Console.SetCursorPosition(X + 1, Y + 1 + i);
                string message = GameLog.Messages[i];
                string filler = "                                                      " +
                    "                                                             ".Substring
                    (0, LogBox.String[0].Length - message.Length);
                Console.Write(message + filler);
            }
            Console.SetCursorPosition(X, Y + 1 + GameLog.logLength);
            Console.Write(LogBox.String[2]);

        }
        private void PrintStatusBar(int X, int Y, int length, ConsoleColor FGcolour, float value, float maxValue)
        {
            int actualLength = (int)(value / maxValue * length);
            StringBuilder builder = new();
            builder.Append('.');
            for (int i = 1; i < actualLength; i++)
            {
                builder.Append('_');
            }
            for (int i = 0; i < length - actualLength; i++)
            {
                builder.Append(' ');
            }
            builder.Append('.');
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = FGcolour;
            Console.Write(builder.ToString());
            Console.ResetColor();
        }

        private void PrintRevealCeilingButton()
        {
            int X = CeilingRevealButton.X;
            int Y = CeilingRevealButton.Y;
            Console.SetCursorPosition(X, Y);
            if (!_game.map.ShowCeiling)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("   / ");
                Console.SetCursorPosition(X, Y + 1);
                Console.Write("  Ø  ");
                Console.SetCursorPosition(X, Y + 2);
                Console.Write(" /");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("U  ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("     ");
                Console.SetCursorPosition(X, Y + 1);
                Console.Write("  O  ");
                Console.SetCursorPosition(X, Y + 2);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("  U  ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void PrintStartScreen()
        {
            Console.Clear();
            PrintGUIElement(StartScreen.String, StartScreen.X, StartScreen.Y);
        }
        private void PrintInventory()
        {
            string[] HandTexts = { "Right Hand:  ", "Left Hand:  " };
            HandInventoryGUI[] HandGUIS = { new RightHandInventoryGUI(), new LeftHandInventoryGUI() };
            for (int i = 0; i < 2; i++)
            {
                Console.SetCursorPosition(HandGUIS[i].X, HandGUIS[i].Y);
                if (_game.player.inventory.Hands[1 - i] != null &&
                    _game.player.inventory.Hands[1 - i].TwoHanded)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(HandTexts[i]);
                Console.Write("                 ");
                Console.SetCursorPosition(HandGUIS[i].X + HandTexts[i].Length, HandGUIS[i].Y);
                Console.ForegroundColor = ConsoleColor.White;

                if (_game.player.inventory.Hands[i] != null)
                {
                    if (_game.player.inventory.Hands[i] == _game.player.inventory.ActiveWeapon)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (_game.player.inventory.Hands[i] == _game.player.inventory.ActiveTool)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write(_game.player.inventory.Hands[i].Description.Split(':')[0]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.SetCursorPosition(PocketsInventoryBox.X, PocketsInventoryBox.Y);
            Console.Write("Pockets:");
            int counter = 1;
            foreach (var item in _game.player.inventory.Pockets)
            {
                Console.SetCursorPosition(PocketsInventoryBox.X, PocketsInventoryBox.Y + counter);
                Console.Write(item.Description.Split(':')[0] + "       ");
                counter++;
            }
            PrintGUIElement(PocketInventoryDeleter.String,
                PocketsInventoryBox.X, PocketsInventoryBox.Y + counter);
        }

        public void PrintMap()
        {
            for (int y = 0; y < MapDisplaySize.Height; y++)
            {
                Point bufferCoord = CameraToBufferPos(0, y);
                Console.SetCursorPosition(bufferCoord.X, bufferCoord.Y);
                for (int x = 0; x < MapDisplaySize.Width; x++)
                {
                    Point mapCoord = CameraToMapPos(x, y);
                    if (_game.map.WithinBounds(mapCoord.X, mapCoord.Y))
                    {
                        ObjectOnMap objectOnMap = _game.map.GetTopObjWithCoord(mapCoord.X, mapCoord.Y);

                        if (objectOnMap.CurrentForegroundColor != null)
                        {
                            (objectOnMap as IDrawable).Write();
                        }
                        else
                        {
                            Console.Write(" ");
                        }


                    }
                }
            }
        }
        public void PrintFlash(ConsoleColor color)
        {
            string[] final = new string[MapDisplaySize.Height];
            for (int rowIndex = 0; rowIndex < MapDisplaySize.Height; rowIndex++)
            {
                StringBuilder row = new();
                for (int columnIndex = 0; columnIndex < MapDisplaySize.Width; columnIndex++)
                {
                    row.Append("█");
                }
                final[rowIndex] = row.ToString();
            }
            Console.ForegroundColor = color;
            PrintGUIElement(final,MapDisplayPosition.TopLeftPosX, MapDisplayPosition.TopLeftPosY);
            Console.ResetColor();
            System.Threading.Thread.Sleep(100);
            PrintGame();
        }

        public void DrawGrabDirectionArrow(Direction direction)
        {
            Point playerBufferPos = MapToBufferPos(_game.player.X,_game.player.Y);
            Point coordDiff = GameMath.DirectionToCoordDiff(direction);
            int X = playerBufferPos.X + coordDiff.X * 2;
            int Y = playerBufferPos.Y + coordDiff.Y * 2;
            char arrow = DirectionArrows.chars[(int)direction];
            Console.SetCursorPosition(X, Y);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(arrow);
        }

        ///<summary>
        ///принимает координаты относительно буфера
        ///</summary>
        public void PrintCellDescription(int X, int Y)
        {
            Point absolutePoint = BufferToMapPos(X, Y);
            string description;

            if (_game.map.InPlayerFOV(absolutePoint.X, absolutePoint.Y))
            {
                description =
                    _game.map.GetTopObjWithCoord(absolutePoint.X, absolutePoint.Y).Description;
                PrintDescriptionBox(description);
            }
        }
        public void PrintInventoryItemDescription(InventoryObject iObject)
        {
            PrintDescriptionBox(iObject.Description
                + " Size: " + GameMath.SizeDescription(iObject.Size) + " ");
        }
        private void PrintDescriptionBox(string description)
        {
            int XOffset = DescriptionBox.String[0].Length / 2;
            int GameFieldCenterX = MapDisplayPosition.TopLeftPosX + MapDisplaySize.Width / 2;

            int DescriptionBoxX = GameFieldCenterX - XOffset;
            int DescriptionBoxY = 4;

            PrintGUIElement(DescriptionBox.String, DescriptionBoxX, DescriptionBoxY);

            string[] SnippedDesc = GameMath.ChunksOf(description, DescriptionBox.textWidth);
            PrintGUIElement(SnippedDesc, DescriptionBoxX + DescriptionBox.textStartOffsetX,
                DescriptionBoxY + DescriptionBox.textStartOffsetY);
        }
        public bool PrintLootableItemList(int x, int y)
        {
            if (!BufferPosInsideDisplayArea(x, y))
            {
                return false;
            }

            Point onMap = BufferToMapPos(x, y);

            if (!_game.map.InPlayerFOV(onMap.X, onMap.Y))
            {
                return false;
            }

            ObjectOnMap obj = _game.map.GetTopObjWithCoord(onMap.X, onMap.Y);

            if ((!(obj is ISearchable)) || ((obj as ISearchable).Inventory.Count == 0))
            {
                return false;
            }

            List<string> itemsWindow = new();
            itemsWindow.Add(ItemListBox.String[0]);
            int itemCount = (obj as ISearchable).Inventory.Count;

            for (int i = 0; i < itemCount; i++)
            {
                itemsWindow.Add(ItemListBox.String[1]);
            }
            itemsWindow.Add(ItemListBox.String[2]);

            Direction directionOfItemList =
                (y - itemsWindow.Count - 1 > MapDisplayPosition.TopLeftPosY ?
                Direction.Up : Direction.Down);


            Point windowPosition = new(0, 0);
            if (directionOfItemList == Direction.Up)
            {
                PrintGUIElement(new string[] { "V" }, x, y - 1);
                windowPosition = new Point(Math.Max(0, x - ItemListBox.boxWidth / 2),
                                             Math.Max(0, y - 3 - itemCount));
            }
            else if (directionOfItemList == Direction.Down)
            {
                PrintGUIElement(new string[] { "Ʌ" }, x, y + 1);
                windowPosition = new Point(Math.Max(0, x - ItemListBox.boxWidth / 2),
                                             Math.Max(0, y + 2));
            }

            PrintGUIElement(new string[] { itemsWindow[0] }, windowPosition.X, windowPosition.Y);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            PrintGUIElement(itemsWindow.GetRange(1, itemCount + 1).ToArray(),
                windowPosition.X, windowPosition.Y + 1);

            List<string> itemNames = new();
            foreach (InventoryObject inv in (obj as ISearchable).Inventory)
            {
                string name = inv.Description.Split(":")[0];
                itemNames.Add(name);
            }
            PrintGUIElement(itemNames.ToArray(), windowPosition.X + 1, windowPosition.Y + 1);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            return true;
        }
        public void PrintHandPopupMenu(int handIndex)
        {

            HandInventoryGUI[] handInventoryGUI = { new RightHandInventoryGUI(),
                                                    new LeftHandInventoryGUI() };
            int X = handInventoryGUI[handIndex].X + HandPopupMenu.arrowOffestX;
            int Y = handInventoryGUI[handIndex].Y + HandPopupMenu.arrowOffestY;
            PrintGUIElement(HandPopupMenu.String, X, Y);
            X += HandPopupMenu.optionStartOffsetX;
            Y += HandPopupMenu.optionStartOffsetY;
            Console.SetCursorPosition(X, Y);
            Console.Write("USE");
            Console.SetCursorPosition(X, ++Y);
            Console.Write("DROP");
            Console.SetCursorPosition(X, ++Y);
            Console.Write("POCKET");
        }
        internal void PrintPocketPopupMenu(int index)
        {
            int X = PocketsInventoryBox.X + PocketPopupMenu.arrowOffestX;
            int Y = PocketsInventoryBox.Y + 1 + index + PocketPopupMenu.arrowOffestY;
            PrintGUIElement(PocketPopupMenu.String, X, Y);
            X += PocketPopupMenu.optionStartOffsetX;
            Y += PocketPopupMenu.optionStartOffsetY;
            Console.SetCursorPosition(X, Y);
            Console.Write("GRAB");
            Console.SetCursorPosition(X, ++Y);
            Console.Write("DROP");
        }
        internal void EraseInventoryPopups()
        {
            HandInventoryGUI[] handInventoryGUI = { new RightHandInventoryGUI(),
                                                    new LeftHandInventoryGUI() };
            int X = handInventoryGUI[1].X + HandPopupMenu.arrowOffestX;
            int Y = handInventoryGUI[1].Y + HandPopupMenu.arrowOffestY;
            PrintGUIElement(PopupMenuDeleter.String, X, Y);
        }

        internal void EraseLootableItemList(LootableItemListDeleter itemListDeleter)
        {
            if (itemListDeleter.PopupDirection == Direction.Down)
            {
                PrintGUIElement(itemListDeleter.GetInStringsArray(), itemListDeleter.X, itemListDeleter.Y);
            }
            else if(itemListDeleter.PopupDirection == Direction.Up)
            {
                PrintGUIElement(itemListDeleter.GetInStringsArray(), itemListDeleter.X,
                    itemListDeleter.Y - itemListDeleter.ItemCount - 3);
            }
        }

        ///<summary>
        ///Переводит координаты относительно буфера в абсолютные координаты карты.
        ///</summary>
        public Point BufferToMapPos(int X, int Y)
        {
            Point output = new Point();
            output = BufferToCameraPos(X, Y);
            output.X += GetCameraPos().X;
            output.Y += GetCameraPos().Y;
            return output;
        }
        public Point BufferToCameraPos(int X, int Y)
        {
            Point output = new();
            output.X = X - MapDisplayPosition.TopLeftPosX;
            output.Y = Y - MapDisplayPosition.TopLeftPosY;
            return output;
        }
        public Point MapToBufferPos(int X, int Y)
        {
            X -= GetCameraPos().X;
            Y -= GetCameraPos().Y;
            Point output = CameraToBufferPos(X, Y);
            return output;
        }
        public Point CameraToBufferPos(int X, int Y)
        {
            Point output = new Point();
            output.X = X + MapDisplayPosition.TopLeftPosX;
            output.Y = Y + MapDisplayPosition.TopLeftPosY;
            return output;
        }

        public Point CameraToMapPos(int X, int Y)
        {
            Point point = CameraToBufferPos(X, Y);
            return BufferToMapPos(point.X, point.Y);
        }
        public Point GetCameraPos()
        {
            Point cameraPos = new();
            cameraPos.X = _game.player.X - MapDisplaySize.Width / 2;
            cameraPos.Y = _game.player.Y - MapDisplaySize.Height / 2;

            cameraPos.X = Math.Max(0, cameraPos.X);
            cameraPos.Y = Math.Max(0, cameraPos.Y);

            cameraPos.X = Math.Min(MapSize.Width - MapDisplaySize.Width, cameraPos.X);
            cameraPos.Y = Math.Min(MapSize.Height - MapDisplaySize.Height, cameraPos.Y);

            return cameraPos;
        }
        public bool BufferPosInsideDisplayArea(int x, int y)
        {
            if (x < MapDisplayPosition.TopLeftPosX ||
                x >= MapDisplayPosition.TopLeftPosX + MapDisplaySize.Width)
            {
                return false;
            }
            if (y < MapDisplayPosition.TopLeftPosY ||
                y >= MapDisplayPosition.TopLeftPosY + MapDisplaySize.Height)
            {
                return false;
            }
            return true;
        }
        private void PrintGUIElement(string[] stringArray, int X, int Y)
        {
            int offset = 0;
            foreach (string row in stringArray)
            {
                Console.SetCursorPosition(X, Y + offset);
                Console.Write(row);
                offset++;
            }
        }
    }
}