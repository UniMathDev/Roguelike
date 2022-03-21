using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.GameConfig
{
    public class LogMessages
    {
        public static string[] MonsterSeesYou =
        {
            "Quick footsteps can be heard closing in on me!",
            "I hear that one of the things is onto me!",
            "One of the things is running straight to me!",
            "Another thing can be heard running toward me!",
        };
        public static string[] DamageTakenFromMonster =
        {
            "Thing bites me!",
            "Thing scratches me!",
            "Thing slashes me!",
            "The thing kicks me!",
            "Thing munches on my leg!",
        };
        public static string[] UseUnsuccessful =
        {
            "I can't do that.",
            "That can't be used in that way.",
            "It's not possible to use it like that.",
        };
        public static string[] DoorOpened =
        {
            "Door sqeaks open.",
            "I push the door open.",
            "I open the door.",
            "The door is now open.",
        };
        public static string[] DoorClosed =
        {
            "Door sqeaks closed.",
            "I shut the door.",
            "I close the door.",
            "The door is now closed.",
        };
    }
}
