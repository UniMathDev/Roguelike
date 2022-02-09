using System;
using Roguelike.Engine;
using Roguelike.GameConfig;
using Roguelike.GameConfig.GUIElements;
using Roguelike.Engine.Monsters;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Roguelike.Client
{
    class GUI
    {
        private readonly Game _game;

        private Point CameraCenterOffset = new Point(GameFieldSize.Width / 2, GameFieldSize.Height / 2);

        public GUI(Game game)
        {
            _game = game;
            Console.CursorVisible = false;
        }

        public void PrintAMove()
        {
            PrintFixedObjects();
            PrintPlayer();
            PrintMonsters();

            Console.SetCursorPosition(0, 0);
            Console.Write(GetCameraPos());
        }

        private void PrintMonsters()
        {
            foreach (Monster monster in _game._monsterManager.monsterList)
            {
                Point CursorPos = MapToBufferPos(monster.X, monster.Y);
                Console.SetCursorPosition(CursorPos.X, CursorPos.Y);
                Console.Write(monster.Character);
            }
        }

        private void PrintPlayer()
        {
            int playerX = ((_game.player.X < CameraCenterOffset.X) ?
                _game.player.X : CameraCenterOffset.X);
            playerX = (_game.player.X >
                (MapSize.Width - (GameFieldSize.Width - CameraCenterOffset.X)) ?
                (CameraCenterOffset.X + _game.player.X -
                (MapSize.Width - (GameFieldSize.Width - CameraCenterOffset.X))) :
                playerX);
            int playerY = ((_game.player.Y < CameraCenterOffset.Y) ?
                _game.player.Y : CameraCenterOffset.Y);
            playerY = (_game.player.Y >
                (MapSize.Height - (GameFieldSize.Height - CameraCenterOffset.Y)) ?
                (CameraCenterOffset.Y + _game.player.Y -
                (MapSize.Height - (GameFieldSize.Height - CameraCenterOffset.Y))) :
                playerY);

            Console.SetCursorPosition(playerX + GameFieldPosition.TopLeftPosX,
                playerY + GameFieldPosition.TopLeftPosY);
            Console.Write(_game.player.Character);
        }

        private void PrintFixedObjects()
        {
            string[] mapInStringArray =
                _game.GetMapInStringArray(_game.player.X - GameFieldSize.Width / 2, _game.player.Y - GameFieldSize.Height / 2, GameFieldSize.Width, GameFieldSize.Height);
            for (int i = 0; i < mapInStringArray.Length; i++)
            {
                Console.SetCursorPosition(GameFieldPosition.TopLeftPosX,
                    GameFieldPosition.TopLeftPosY + i);
                Console.Write(mapInStringArray[i]);
            }
        }

        ///<summary>
        ///принимает координаты относительно буфера
        ///</summary>
        public void PrintCellDescription(int X, int Y)
        {
            Point absolutePoint = BufferToMapCoord(X, Y);
            if (_game._map.WithinBounds(absolutePoint.X, absolutePoint.Y))
            {
                string description = _game._map.GetObjWithCoord(absolutePoint.X, absolutePoint.Y).Description;
                int XOffset = DescriptionBox.String[0].Length / 2;
                int GameFieldCenterX = GameFieldPosition.TopLeftPosX + GameFieldSize.Width / 2;

                int DescriptionBoxX = GameFieldCenterX - XOffset;
                int DescriptionBoxY = 4;

                PrintGUIElement(DescriptionBox.String, DescriptionBoxX, DescriptionBoxY);

                string[] SnippedDesc = GameMath.ChunksOf(description, DescriptionBox.textWidth);
                PrintGUIElement(SnippedDesc, DescriptionBoxX + DescriptionBox.textStartOffsetX,
                    DescriptionBoxY + DescriptionBox.textStartOffsetY);
            }
            //Console.Write(description);
        }
        ///<summary>
        ///Переводит координаты относительно буфера в абсолютные координаты карты.
        ///</summary>
        private Point BufferToMapCoord(int X, int Y)
        {
            Point output = new Point();
            output = BufferToCameraPos(X,Y);
            output.X += GetCameraPos().X;
            output.Y += GetCameraPos().Y;
            return output;
        }
        private Point BufferToCameraPos(int X, int Y)
        {
            Point output = new();
            output.X = X - GameFieldPosition.TopLeftPosX;
            output.Y = Y - GameFieldPosition.TopLeftPosY;
            return output;
        }
        //
        private Point MapToBufferPos(int X, int Y)
        {
            X -= GetCameraPos().X;
            Y -= GetCameraPos().Y;
            Point output = CameraToBufferPos(X, Y);
            return output;
        }
        private Point CameraToBufferPos(int X, int Y)
        {
            Point output = new Point();
            output.X = X + GameFieldPosition.TopLeftPosX;
            output.Y = Y + GameFieldPosition.TopLeftPosY;
            return output;
        }
        private Point GetCameraPos()
        {
            Point cameraPos = new();
            cameraPos.X = _game.player.X - GameFieldSize.Width / 2;
            cameraPos.Y  = _game.player.Y - GameFieldSize.Height / 2;

            cameraPos.X = Math.Max(0, cameraPos.X);
            cameraPos.Y = Math.Max(0, cameraPos.Y); 

            cameraPos.X = Math.Min(MapSize.Width - GameFieldSize.Width, cameraPos.X);
            cameraPos.Y = Math.Min(MapSize.Height - GameFieldSize.Height, cameraPos.Y);

            return cameraPos;
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