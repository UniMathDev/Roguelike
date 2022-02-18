using Roguelike.Engine.ObjectsOnMap;
using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Roguelike.Engine.Maps
{
    public static class TxtToMapConverter
    {
        public static Map ConvertToArrayMap(string pathToFileWithMap, int height, int width)
        {
            var objectsOnMap = new ObjectOnMap[height, width];
            var availableCharsOfObjs = new CharsOfObjects();
            List<Egg> eggList = new List<Egg>();

            using var streamReader = new StreamReader(pathToFileWithMap, Encoding.UTF8);

            string[] mapInStrings = streamReader.ReadToEnd().Split('\n');
            for (int y = 0; y < height; y++)
            {
                char[] objSymbols = mapInStrings[y].ToCharArray();
                for(int x = 0; x < width; x++)
                {
                    char objSymbol = objSymbols[x];
                    ObjectOnMap objectToBeCreated = availableCharsOfObjs.GetObjForChar(objSymbol);
                    if (objectToBeCreated is Egg)
                    {
                        Egg egg = objectToBeCreated as Egg;
                        egg.X = x;
                        egg.Y = y;
                        eggList.Add(egg);
                    }
                    objectsOnMap[y, x] = objectToBeCreated;
                }
            }
            Map map = new(height, width, objectsOnMap, eggList);
            return map;
        }
    }
}
