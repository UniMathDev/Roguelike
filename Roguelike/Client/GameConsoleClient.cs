using Roguelike.Engine;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.GameConfig;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Roguelike.Client
{
    class GameConsoleClient
    {
        private readonly Dictionary<ConsoleKey, GameMenuItem> _menu;
        private readonly Game _game;

        public GameConsoleClient()
        {
            Map map = TxtToMapConverter.ConvertToArrayMap(@"..\..\..\..\Maps\TestMap1.txt", MapSize.Height, MapSize.Width);
            Player player = new(PlayerInitCoords.X, PlayerInitCoords.Y);
            _game = new(map, player);

            _menu = new Dictionary<ConsoleKey, GameMenuItem>();
            _menu.Add(ConsoleKey.Escape, new GameMenuItem(Exit));
            _menu.Add(ConsoleKey.UpArrow, new GameMenuItem(MoveUp));
            _menu.Add(ConsoleKey.DownArrow, new GameMenuItem(MoveDown));
            _menu.Add(ConsoleKey.RightArrow, new GameMenuItem(MoveRight));
            _menu.Add(ConsoleKey.LeftArrow, new GameMenuItem(MoveLeft));
        }

        public void Start()
        {
            Console.CursorVisible = false;
            PrintAMove();
            ConsoleKey key = ConsoleKey.F24;
            bool inGame = true;
            
            while (inGame)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }

                if (_menu.ContainsKey(key))
                {
                    _menu[key].Action();
                }                

                key = ConsoleKey.F24;
                Thread.Sleep(16);
            }
        }


        private void MoveUp()
        {
            _game.Move(Directions.Up);
            PrintAMove();
        }

        private void MoveDown()
        {
            _game.Move(Directions.Down);
            PrintAMove();
        }

        private void MoveRight()
        {
            _game.Move(Directions.Right);
            PrintAMove();
        }

        private void MoveLeft()
        {
             _game.Move(Directions.Left);
            PrintAMove();
        }

        private void PrintAMove()
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

        private void Exit()
        {
            Console.Clear();
            Environment.Exit(0);
        }

    }
}
