using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using System.IO;
using System.Text;
using System;
using Roguelike.Engine.InventoryObjects;

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

            string[] lootMapInStrings = sr.ReadToEnd().Split('\n');
            for (int y = 0; y < height; y++)
            {
                char[] NumbersOfScript = lootMapInStrings[y].ToCharArray();
                for (int x = 0; x < width; x++)
                {
                    int numberOfScript;
                    try
                    {
                        numberOfScript = Convert.ToInt32(NumbersOfScript[x]);
                        LootMap[y, x] = numberOfScript;
                    }
                    catch
                    {
                        throw new ArgumentException("Error in converting from LootMap");
                    }

                    //foreach ( ... i in CreateLoot(.../numberOfScript))
                    {

                    }
                    /*
                    if (numberOfScript == 1) //Max Weapon
                    {
                        //bool knockedBack = weapon.KnockBackChance - (float)GameMath.rand.NextDouble() >= 0f;
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Axe());
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new KitchenKnife());
                    }
                    else if (numberOfScript == 2) //Канцелярия, рабочее место, и т.д.
                    {
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                        (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new LightBulb());
                    }
                    else if (numberOfScript == 3)
                    {

                    }
                    */
                    //массив скриптов
                    /*
                    Type typeOfObject = mapCells[y, x].Layers[1].GetType();
                    switch (typeOfObject)
                    {
                        case Wardrobe:// ---> не может обработать?
                            // ---> вызов функции-оборотня, но тогда пропадет проверка на соответствие!!!

                            break;
                        case Deadbody:
                            //
                            break;
                        case Fridge:
                            //
                            break;
                        case Hanger:
                            //
                            break;
                        case Screen:
                            //
                            break;
                        case CenterOfTable:
                            //
                            break;
                    }
                    */

                    /*
                    //сделать отдельной функцией
                    Type typeOfObject = mapCells[y, x].Layers[1].GetType();
                    if (typeOfObject == typeof(Wardrobe))
                    {

                    }
                    else if (typeOfObject == typeof(Deadbody))
                    {

                    }
                    else if (typeOfObject == typeof(Fridge))
                    {

                    }
                    else if (typeOfObject == typeof(Hanger))
                    {

                    }
                    else if (typeOfObject == typeof(Screen))
                    {

                    }
                    else if (typeOfObject == typeof(CenterOfTable))
                    {

                    }
                    else
                    {

                    }
                    */

                }
            }
        }

    }
}
