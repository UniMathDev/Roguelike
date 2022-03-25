using Roguelike;
using Roguelike.Engine;

namespace Roguelike.Engine
{
    public static class GameLog
    {
        public const int logLength = 6;
        public static string[] Messages { get; private set; } = new string[logLength];
        public static void Start()
        {
            for (int i = 0; i < logLength; i++)
            {
                Messages[i] = "";
            }
        }
        public static void Add(string Message)
        {
            for (int i = 1; i < logLength ; i++)
            {
                Messages[i - 1] = Messages[i];
            }
            Messages[logLength - 1] = Message;
        }
        public static void Add(params string[] Message)
        {
            int randomNum = GameMath.rand.Next(0, Message.Length);
            Add(Message[randomNum]);
        }
    }
}
