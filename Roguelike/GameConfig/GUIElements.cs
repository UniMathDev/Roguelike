namespace Roguelike.GameConfig.GUIElements
{
    class GUIElement {
        public static string[] String;
    }
    class DescriptionBox : GUIElement
    {
        new public static readonly string[] String = new string[8]
        {
            "___________________________",
            "|                         |",
            "|                         |",
            "|                         |",
            "|                         |",
            "|                         |",
            "|                         |",
            "|_________________________|"
        };
        public const int textStartOffsetX = 1;
        public const int textStartOffsetY = 1;
        public const int textWidth = 25;
        public const int textHeight = 6;
        
    }
}
