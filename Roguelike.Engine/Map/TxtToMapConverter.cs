using Roguelike.Engine.ObjectsOnMap;
using System.IO;
using System.Text;

namespace Roguelike.Engine.Map
{
    class TxtToMapConverter : IMapConverter
    {
        public Map ConvertToMap(string pathToFileWithMap, int height, int width)
        {
            var objectsOnMap = new ObjectOnMap[height, width];
            var availableCharsOfObjs = new CharsOfObjects();

            using var streamReader = new StreamReader(pathToFileWithMap, Encoding.UTF8);

            string[] mapInStrings = streamReader.ReadToEnd().Split('\n');
            for (int y = 0; y < height; y++)
            {
                char[] objSymbols = mapInStrings[y].ToCharArray();
                for(int x = 0; x < width; x++)
                {
                    char objSymbol = objSymbols[x];
                    objectsOnMap[x, y] = availableCharsOfObjs.GetObjForChar(objSymbol);
                }
            }

            MapInArray map = new(height, width, objectsOnMap);
            return map as Map;
        }
    }
}
