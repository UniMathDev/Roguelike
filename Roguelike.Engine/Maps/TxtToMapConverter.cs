using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using System.IO;
using System.Text;
using System;
using Roguelike.Engine.InventoryObjects;
using System.Collections.Generic;
using Roguelike.Engine.Enums;

namespace Roguelike.Engine.Maps
{
    public static class TxtToMapConverter
    {
        public static Map ConvertToArrayMap(string pathToFileWithMap, int height, int width)
        {
            MapCell[,] mapCells = CreateMapToArray(pathToFileWithMap, height, width);

            CreateLootMapToArray(mapCells, height, width);

            Map map = new(height, width, mapCells);
            return map;
        }

        //
        public static MapCell[,] CreateMapToArray (string pathToFileWithMap, int height, int width)
        {
            MapCell[,] mapCells = new MapCell[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    mapCells[y, x] = new MapCell();
                }
            }
            var availableCharsOfObjs = new CharsOfObjects();

            using var streamReader = new StreamReader(pathToFileWithMap, Encoding.UTF8);

            string[] mapInStrings = streamReader.ReadToEnd().Split('\n');
            for (int y = 0; y < height; y++)
            {
                char[] objSymbols = mapInStrings[y].ToCharArray();
                for (int x = 0; x < width; x++)
                {
                    char objSymbol = objSymbols[x];
                    ObjectOnMap objectToBeCreated = availableCharsOfObjs.GetObjForChar(objSymbol);

                    if (objectToBeCreated is Egg)
                    {
                        Egg egg = objectToBeCreated as Egg;
                        egg.X = x;
                        egg.Y = y;
                    }

                    mapCells[y, x].Layers[(int)objectToBeCreated.MapLayer] = objectToBeCreated;
                }
            }

            return mapCells;
        }

        public static void CreateLootMapToArray(MapCell[,] mapCells, int height, int width)
        {
            using var sr = new StreamReader(@"..\..\..\..\Maps\LootMap.txt", Encoding.UTF8);
            int[,] LootMap = new int[height, width];

            var ElementToSripts = new Loot();

            string[] lootMapInStrings = sr.ReadToEnd().Split('\n');
            for (int y = 0; y < height; y++)
            {
                char[] NumbersOfScript = lootMapInStrings[y].ToCharArray();
                for (int x = 0; x < width; x++)
                {
                    int numberOfScript;
                    try
                    {
                        //нельзя char --> int, только из string
                        numberOfScript = Convert.ToInt32(NumbersOfScript[x].ToString()); 
                        LootMap[y, x] = numberOfScript;
                    }
                    catch
                    {
                        throw new ArgumentException("Error in converting from LootMap");
                    }

                    if (numberOfScript != 0)
                    {
                        List<InventoryObject> FinalItems = ElementToSripts.CreateLoot(numberOfScript);
                        foreach (var i in FinalItems)
                        {
                            (mapCells[y, x].Layers[(int)MapLayer.MAIN] as ISearchable).Inventory.Add(i);
                        }
                    }

                }
            }

        }

    }
}
