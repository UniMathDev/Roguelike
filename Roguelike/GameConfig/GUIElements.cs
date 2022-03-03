namespace Roguelike.GameConfig.GUIElements
{
    class GUIElement {
        public static string[] String;
    }
    class DescriptionBox : GUIElement
    {
        new public static readonly string[] String = new string[9]
        {
            "___________________________",
            "|                         |",
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
        public const int textHeight = 7;
        
    }
    class ItemListBox : GUIElement
    {
        new public static readonly string[] String = new string[3]
        {
            "_________________",
            "|               |",
            "|_______________|",
        };
        public const int boxWidth = 17;
        public const int textStartOffsetX = 1;
    }
    class RevealCeilingButton : GUIElement
    {
        /*
        public static readonly string[] StringEnabled = new string[3]
        {
            @"   / ",
            @"  Ø  ",
            @" /U  ",
        };
        public static readonly string[] StringDisabled = new string[3]
        {
            "     ",
            "  O  ",
            "  U  ",
        };
        */
        public const int width = 5;
        public const int height = 3;
        public const int X = 45;
        public const int Y = 4;
    }
}
