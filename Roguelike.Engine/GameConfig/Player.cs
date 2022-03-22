namespace Roguelike.GameConfig
{
    public struct PlayerInit
    {
        public const int X = 11;
        public const int Y = 3;
        public const int FOVSize = 30;
    }
    public struct PlayerStats
    {
        public const int PocketSize = 5;
        public const float MaxHealth = 100;
        public const float MaxStamina = 100;
        public const float RunStaminaPenalty = 20;
        public const float WalkStaminaPenalty = 5;
        public const float EndOfTurnStaminaGain = 7;
        public const float FistDamage = 30;
    }
}
