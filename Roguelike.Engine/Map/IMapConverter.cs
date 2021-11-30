namespace Roguelike.Engine.Map
{
    interface IMapConverter
    {
        public Map ConvertToMap(string pathToFileWithMap, int height, int width);
    }
}
