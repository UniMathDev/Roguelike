using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using System.IO;
using System.Text;

namespace Roguelike.Engine.Map
{
    class TxtToMapConverter : IMapConverter
    {
        public Map ConvertToMap(string pathToFileWithMap, int height, int width)
        {
            var objectsOnMap = new ObjectOnMap[height, width];
            FixedObjectFactory factory = new();

            using var streamReader = new StreamReader(pathToFileWithMap, Encoding.UTF8);

            string[] mapInStrings = streamReader.ReadToEnd().Split('\n');
            for (int y = 0; y < height; y++)
            {
                char[] objSymbols = mapInStrings[y].ToCharArray();
                for(int x = 0; x < width; x++)
                {
                    char objSymbol = objSymbols[x];
                    objectsOnMap[x, y] = factory.GetFixedObject(objSymbol);
                }
            }

            MapInArray map = new(height, width, objectsOnMap);
            return map as Map;
        }
    }
}
