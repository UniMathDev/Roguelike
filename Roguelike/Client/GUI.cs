using System;
using Roguelike.Engine;
using Roguelike.Engine.ObjectsOnMap;
using Roguelike.GameConfig;
using Roguelike.GameConfig.GUIElements;
using Roguelike.Engine.Monsters;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using System.Drawing;
using System.Text;
using System.Collections.Generic;

namespace Roguelike.Client
{
    class GUI
    {
        private readonly Game _game;

        private Point CameraCenterOffset = new Point(MapDisplaySize.Width / 2, MapDisplaySize.Height / 2);

        public GUI(Game game)
        {
            _game = game;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;
        }

        public void PrintGame()
        {
            PrintInventory();
            PrintMap();
            PrintRevealCeilingButton();

            Console.SetCursorPosition(0, 0);
            Console.Write(_game.player.Health + " ");
        }

        private void PrintRevealCeilingButton()
        {
            int X = RevealCeilingButton.X;
            int Y = RevealCeilingButton.Y;
            Console.SetCursorPosition(X, Y);
            if (!_game._map.ShowCeiling)
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

        private void PrintInventory()
        {
            Console.SetCursorPosition(80, 2);
            if(_game.player.inventory.Hands[0] != null)
                Console.WriteLine("Right Hand:  " + _game.player.inventory.Hands[0].Description.Split(':')[0]);
            Console.SetCursorPosition(80, 3);
            if (_game.player.inventory.Hands[1] != null)
                Console.WriteLine("Left Hand:  " + _game.player.inventory.Hands[1].Description.Split(':')[0]);
            Console.SetCursorPosition(80, 4);
            Console.WriteLine("Pockets:");
            int counter = 5;
            foreach (var item in _game.player.inventory.Pockets)
            {
                Console.SetCursorPosition(80, counter);
                Console.WriteLine(item.Description.Split(':')[0]);
                counter++;
            }
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
                    if (_game._map.WithinBounds(mapCoord.X, mapCoord.Y))
                    {
                        ObjectOnMap objectOnMap = _game._map.GetTopObjWithCoord(mapCoord.X, mapCoord.Y);

                        (objectOnMap as IDrawable).Write();
                    }
                }
            }
        }
        ///<summary>
        ///принимает координаты относительно буфера
        ///</summary>
        public void PrintCellDescription(int X, int Y)
        {
            Point absolutePoint = BufferToMapPos(X, Y);
            string description = string.Empty;

            if (_game._map.WithinBounds(absolutePoint.X, absolutePoint.Y))
            {
                foreach (Monster monster in _game._monsterManager.monsterList)
                {
                    if (monster.coordinates == absolutePoint)
                    {
                        description = monster.Description;
                    }
                }
                if(description == string.Empty)
                description = _game._map.GetTopObjWithCoord(absolutePoint.X, absolutePoint.Y).Description;
                int XOffset = DescriptionBox.String[0].Length / 2;
                int GameFieldCenterX = MapDisplayPosition.TopLeftPosX + MapDisplaySize.Width / 2;

                int DescriptionBoxX = GameFieldCenterX - XOffset;
                int DescriptionBoxY = 4;

                PrintGUIElement(DescriptionBox.String, DescriptionBoxX, DescriptionBoxY);

                string[] SnippedDesc = GameMath.ChunksOf(description, DescriptionBox.textWidth);
                PrintGUIElement(SnippedDesc, DescriptionBoxX + DescriptionBox.textStartOffsetX,
                    DescriptionBoxY + DescriptionBox.textStartOffsetY);
            }
            //Console.Write(description);
        }

        public bool PrintGroundItemList(int X, int Y)
        {
            if (!BufferPosInsideDisplayArea(X,Y))
            {
                return false;
            }
            Point onMap = BufferToMapPos(X,Y);
            ObjectOnMap obj = _game._map.GetTopObjWithCoord(onMap.X, onMap.Y);
            if (!(obj is InventoryObjectOnGround))
            {
                return false;
            }

            List<string> itemsWindow = new();
            itemsWindow.Add(ItemListBox.String[0]);
            int itemCount = (obj as InventoryObjectOnGround).Inventory.Count;
            for (int i = 0; i < itemCount; i++)
            {
                itemsWindow.Add(ItemListBox.String[1]);
            }
            itemsWindow.Add(ItemListBox.String[2]);
            PrintGUIElement(itemsWindow.ToArray(), X - ItemListBox.boxWidth / 2, Y - 3 - itemCount);
            string[] arrow = { "V" };
            PrintGUIElement(arrow,X,Y - 1);

            List<string> itemNames = new();
            foreach (var inv in (obj as InventoryObjectOnGround).Inventory)
            {
                string name = inv.Description.Split(":")[0];
                itemNames.Add(name);
            }
            PrintGUIElement(itemNames.ToArray(), X - ItemListBox.boxWidth / 2 + 1, Y - 2 - itemCount);

            return true;
        }

        ///<summary>
        ///Переводит координаты относительно буфера в абсолютные координаты карты.
        ///</summary>
        public Point BufferToMapPos(int X, int Y)
        {
            Point output = new Point();
            output = BufferToCameraPos(X,Y);
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
            cameraPos.Y  = _game.player.Y - MapDisplaySize.Height / 2;

            cameraPos.X = Math.Max(0, cameraPos.X);
            cameraPos.Y = Math.Max(0, cameraPos.Y); 

            cameraPos.X = Math.Min(MapSize.Width - MapDisplaySize.Width, cameraPos.X);
            cameraPos.Y = Math.Min(MapSize.Height - MapDisplaySize.Height, cameraPos.Y);

            return cameraPos;
        }
        public bool BufferPosInsideDisplayArea(int x, int y)
        {
            if (x < MapDisplayPosition.TopLeftPosX || x > MapDisplayPosition.TopLeftPosX + MapDisplaySize.Width)
            {
                return false;
            }
            if (y < MapDisplayPosition.TopLeftPosY || y > MapDisplayPosition.TopLeftPosY + MapDisplaySize.Height)
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
                Console.SetCursorPosition(X,Y + offset);
                Console.Write(row);
                offset++;
            }
        }
    }
}