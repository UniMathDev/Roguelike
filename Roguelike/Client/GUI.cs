using System;
using Roguelike.Engine;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.GameConfig;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

namespace Roguelike.Client
{
    class GUI
    {
        private readonly Game _game;

        public GUI(Game game)
        {
            _game = game;
            Console.CursorVisible = false;
        }

        public void PrintAMove()
        {
            Point CameraCenterOffset = new Point(GameFieldSize.Width / 2, GameFieldSize.Height / 2);

            int xPosMap = _game.player.X - CameraCenterOffset.X;
            int yPosMap = _game.player.Y - CameraCenterOffset.Y;
            string[] mapInStringArray =
                _game.GetMapInStringArray(xPosMap, yPosMap, GameFieldSize.Width, GameFieldSize.Height);
            for (int i = 0; i < mapInStringArray.Length; i++)
            {
                Console.SetCursorPosition(GameFieldPosition.TopLeftPosX,
                    GameFieldPosition.TopLeftPosY + i);
                Console.Write(mapInStringArray[i]);
            }

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


    }
}

