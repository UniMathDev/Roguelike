using Roguelike.Client;

namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
        {   
            GameConsoleClient client = new();
            client.Start();
        }
    }
}
