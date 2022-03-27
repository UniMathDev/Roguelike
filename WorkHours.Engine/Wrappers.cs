using Roguelike.Engine.Enums;

namespace WorkHours.Engine
{
    public class IntWrapper
    {
        public int Value { get; set; }
        public IntWrapper(int value) { Value = value; }
    }
    public class DirectionWrapper
    {
        public Direction Value { get; set; }
        public DirectionWrapper(Direction value) { Value = value; }
    }
}
