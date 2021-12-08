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
        private Stack<MapDiff> _mapDiffs;

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
            _mapDiffs = new();
            _mapDiffs.Push(_game.InitPlayer());

            Console.CursorVisible = false;
            Console.WriteLine(_game.MapInString);
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

                while (_mapDiffs.Count > 0)
                {
                    MapDiff mapDiff = _mapDiffs.Pop();
                    Console.SetCursorPosition(mapDiff.X, mapDiff.Y);
                    Console.Write(mapDiff.Character);
                }
                key = ConsoleKey.F24;
                Thread.Sleep(16);
            }
        }


        private void MoveUp() =>
            PushToMapDiffs(Directions.Up);

        private void MoveDown() =>
            PushToMapDiffs(Directions.Down);

        private void MoveRight() =>
            PushToMapDiffs(Directions.Right);

        private void MoveLeft() =>
            PushToMapDiffs(Directions.Left);

        private void Exit()
        {
            Console.Clear();
            Environment.Exit(0);
        }

        private void PushToMapDiffs(Directions direction)
        {
            foreach (var mapDiff in _game.Move(direction))
            {
                _mapDiffs.Push(mapDiff);
            }
        }

    }
}
