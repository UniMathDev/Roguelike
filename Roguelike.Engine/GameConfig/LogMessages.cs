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
        public static string[] PistolHit =
        {
            "I hit it.",
            "I hit it.",
            "It was a hit.",
            "It was a hit.",
            "The crosshair was straight on that thing.",
            "The thing shrivels in pain, a hit.",
            "That was a clear hit.",
            "That was a clear hit.",
        };
        public static string[] PistolMiss =
        {
            "I barely miss it",
            "Thing suddenly changes direction and I miss",
            "I missed.",
            "I missed it.",
            "No effect, it was a miss.",
        };
        public static string[] PistolEmpty =
        {
            "A click is all that comes out of my gun",
            "Click! Gun's empty.",
            "Click! My weapon is empty",
        };
        public static string[] Healed =
        {
            "I treat my wounds.",
        };
    }
}
