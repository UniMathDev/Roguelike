using System;
using Roguelike.Engine;
using Roguelike.Engine.ObjectsOnMap;
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

        private Point CameraCenterOffset = new Point(MapDisplaySize.Width / 2, MapDisplaySize.Height / 2);

        public GUI(Game game)
        {
            _game = game;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;
        }

        public void PrintGame()
        {
            PrintFixedObjects();
            PrintPlayer();
            PrintMonsters();

            Console.SetCursorPosition(0, 0);
            Console.Write(_game.player.health);
        }

        private void PrintMonsters()
        {
            foreach (Monster monster in _game._monsterManager.monsterList)
            {
                //if (InsideMapDisplayArea(monster.X,monster.Y)) 
                {
                    Point CursorPos = MapToBufferPos(monster.X, monster.Y);
                    Console.SetCursorPosition(CursorPos.X, CursorPos.Y);
                    (monster as IDrawable).Write();
                }
            }
        }

        private void PrintPlayer()
        {
            int playerX = ((_game.player.X < CameraCenterOffset.X) ?
                _game.player.X : CameraCenterOffset.X);
            playerX = (_game.player.X >
                (MapSize.Width - (MapDisplaySize.Width - CameraCenterOffset.X)) ?
                (CameraCenterOffset.X + _game.player.X -
                (MapSize.Width - (MapDisplaySize.Width - CameraCenterOffset.X))) :
                playerX);
            int playerY = ((_game.player.Y < CameraCenterOffset.Y) ?
                _game.player.Y : CameraCenterOffset.Y);
            playerY = (_game.player.Y >
                (MapSize.Height - (MapDisplaySize.Height - CameraCenterOffset.Y)) ?
                (CameraCenterOffset.Y + _game.player.Y -
                (MapSize.Height - (MapDisplaySize.Height - CameraCenterOffset.Y))) :
                playerY);

            Console.SetCursorPosition(playerX + MapDisplayPosition.TopLeftPosX,
                playerY + MapDisplayPosition.TopLeftPosY);
            Console.Write(_game.player.Character);
        }

        private void PrintFixedObjects()
        {
            string[] mapInStringArray =
                _game.GetMapInStringArray(_game.player.X - MapDisplaySize.Width / 2, _game.player.Y - MapDisplaySize.Height / 2, MapDisplaySize.Width, MapDisplaySize.Height);
            for (int y = 0; y < mapInStringArray.Length; y++)
            {
                Point bufferCoord = CameraToBufferPos(0, y);
                Console.SetCursorPosition(bufferCoord.X, bufferCoord.Y);
                for (int x = 0; x < mapInStringArray[0].Length; x++)
                {
                    Point mapCoord = CameraToMapPos(x, y);
                    if (_game._map.WithinBounds(mapCoord.X,mapCoord.Y))
                    {
                        ObjectOnMap objectOnMap = _game._map.GetObjWithCoord(mapCoord.X, mapCoord.Y);
                    
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
                description = _game._map.GetObjWithCoord(absolutePoint.X, absolutePoint.Y).Description;
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
        //
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