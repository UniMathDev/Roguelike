﻿using Roguelike.Engine.Enums;
using System.Text;

namespace Roguelike.GameConfig.GUIElements
{
    public class GUIElement
    {
        public static string[] String;
    }
    public class DescriptionBox : GUIElement
    {
        new public static readonly string[] String = new string[9]
        {
            " _________________________ ",
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
    public class ItemListBox : GUIElement
    {
        new public static readonly string[] String = new string[3]
        {
            " _______________ ",
            "|               |",
            "|_______________|",
        };
        public const int boxWidth = 17;
        public const int textStartOffsetX = 1;
    }
    public class CeilingRevealButton : GUIElement
    {
        public const int width = 5;
        public const int height = 3;
        public const int X = 32;
        public const int Y = 3;
    }
    public class HandInventoryGUI : GUIElement
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public static HandInventoryGUI GetHand(int i)
        {
            if (i == 0)
            {
                return new RightHandInventoryGUI();
            }
            if (i == 1)
            {
                return new LeftHandInventoryGUI();
            }
            throw new System.Exception
                ("HandInventoryBox GUI element can't be returned: index must be 1 or 0.");
        }
    }
    public class LeftHandInventoryGUI : HandInventoryGUI
    {
        public LeftHandInventoryGUI()
        {
            X = 102;
            Y = 10;
            Width = 20;
            Height = 1;
        }
    }
    public class RightHandInventoryGUI : HandInventoryGUI
    {
        public RightHandInventoryGUI()
        {
            X = 102;
            Y = 11;
            Width = 20;
            Height = 1;
        }
    }
    public class PocketsInventoryBox : GUIElement
    {
        public const int X = 102;
        public const int Y = 12;
        public const int EntryWidth = 12;
        public const int EntryHeight = 1;

    }
    public class PocketPopupMenu : GUIElement
    {
        new public static readonly string[] String = new string[4]
        {
            " ____/>",
            "|    |",
            "|    |",
            "|____|",
        };
        public const int numberOfOptions = 2;
        public const int boxWidth = 6;
        public const int optionStartOffsetX = 1;
        public const int optionStartOffsetY = 1;
        public const int arrowOffestX = -7;
        public const int arrowOffestY = 0;
    }
    public class HandPopupMenu : GUIElement
    {
        new public static readonly string[] String = new string[5]
        {
            " ______/>",
            "|      |",
            "|      |",
            "|      |",
            "|______|",
        };
        public const int numberOfOptions = 3;
        public const int boxWidth = 8;
        public const int optionStartOffsetX = 1;
        public const int optionStartOffsetY = 1;
        public const int arrowOffestX = -9;
        public const int arrowOffestY = 0;
    }
    public class PopupMenuDeleter : HandPopupMenu
    {
        new public static readonly string[] String = new string[15]
        {
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
            "|        ",
        };
    }

    public class LootableItemListDeleter
    {
        public LootableItemListDeleter(int x, int y, int itemCount, Direction direction)
        {
            X = x - ItemListBox.boxWidth / 2;
            Y = y;
            ItemCount = itemCount;
            PopupDirection = direction;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int ItemCount { get; set; }

        public Direction PopupDirection { get; set; }

        public string[] GetInStringsArray()
        {
            var deleter = new string[ItemCount + 4];
            var space = " ";
            var str = "";

            for(int i = 0; i < ItemListBox.boxWidth; i++)
            {
                str += space;
            }

            for(int i = 0; i < ItemCount + 4; i++)
            {
                deleter[i] = str;
            }

            return deleter;
        }
    }

    public class PocketInventoryDeleter : GUIElement
    {
        new public static readonly string[] String = new string[10]
        {
            "               ",
            "               ",
            "               ",
            "               ",
            "               ",
            "               ",
            "               ",
            "               ",
            "               ",
            "               ",
        };

    }
    public class StartScreen : GUIElement
    {
        new public static readonly string[] String = 
        {
            @"                                                                            ",
            @"                                                                           ",
            @"                                                                            ",
            @"              M O U S E                       K E Y B O A R D                  ",
            @"                 ___                                                         ",
            @"    attack/use> /_|_\ <inspect             numpad 1-9  -  move                ",
            @"               |     |                        or                              ",
            @"               |     |                     arrow keys                          ",
            @"                \___/                      alt         -  run                ",
            @"                                           numpad 0    -  wait to restore        ",
            @"                                                          stamina              ",
            @"                                           esc         -  exit             ",
            @"                                           R           -  reload          ",
            @"                                                                         ",
            @"                      PRESS F1 TO SHOW THIS AGAIN               ",
            @"                      PRESS ANY KEY TO START                                 ",
            @"                                                                          ",
        };
        public const int X = 20;
        public const int Y = 10;
    }
    public class StatusBars : GUIElement
    {
        public const int X = 32;
        public const int Y = 30;
        public const int Length = 61;
    }
    public class LogBox : GUIElement
    {
        public const int X = 38;
        public const int Y = 2;
        new public static readonly string[] String = new string[3]
        {
            " ___________________________________________ ",
            "|                                           |",
            "|___________________________________________|",
        };
    }
    public class DirectionArrows : GUIElement
    {
        public static readonly char[] chars = new char[8]
        {
            '8',
            '9',
            '6',
            '3',
            '2',
            '1',
            '4',
            '7',
        };
    }
    /*

            '∑',
            '̥',
            '2',
            '→',
            '.',
    */
}
