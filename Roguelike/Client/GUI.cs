using System;
using Roguelike.Engine;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.GameConfig;
using System.Collections.Generic;
using System.Threading;

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
            int xPosMap = _game.player.X - PlayerInitCoords.X;
            int yPosMap = _game.player.Y - PlayerInitCoords.Y;
            string[] mapInStringArray =
                _game.GetMapInStringArray(xPosMap, yPosMap, GameFieldSize.Width, GameFieldSize.Height);
            for (int i = 0; i < mapInStringArray.Length; i++)
            {
                Console.SetCursorPosition(GameFieldPosition.TopLeftPosX,
                    GameFieldPosition.TopLeftPosY + i);
                Console.Write(mapInStringArray[i]);
            }

            int playerX = ((_game.player.X < PlayerInitCoords.X) ?
                _game.player.X : PlayerInitCoords.X);
            playerX = (_game.player.X >
                (MapSize.Width - (GameFieldSize.Width - PlayerInitCoords.X)) ?
                (PlayerInitCoords.X + _game.player.X -
                (MapSize.Width - (GameFieldSize.Width - PlayerInitCoords.X))) :
                playerX);
            int playerY = ((_game.player.Y < PlayerInitCoords.Y) ?
                _game.player.Y : PlayerInitCoords.Y);
            playerY = (_game.player.Y >
                (MapSize.Height - (GameFieldSize.Height - PlayerInitCoords.Y)) ?
                (PlayerInitCoords.Y + _game.player.Y -
                (MapSize.Height - (GameFieldSize.Height - PlayerInitCoords.Y))) :
                playerY);
            Console.SetCursorPosition(playerX + GameFieldPosition.TopLeftPosX,
                playerY + GameFieldPosition.TopLeftPosY);
            Console.Write(_game.player.Character);

        }


    }
}
