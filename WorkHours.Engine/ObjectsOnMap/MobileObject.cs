using System.Drawing;
using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;

namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class MobileObject : ObjectOnMap, IChangeAble
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Point Position
        {
            get
            {
                return new Point(X, Y);
            }
            protected set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public bool Draggable = false;
        public MobileObject()
        {
            MapLayer = MapLayer.MAIN;
        }
        public bool CanMove(Direction direction, Map map)
        {
            Point coordDiff = GameMath.DirectionToCoordDiff(direction);
            Point movingTo = new(this.X + coordDiff.X, this.Y + coordDiff.Y);
            return map.IsPossibleToMove(movingTo.X, movingTo.Y);
        }
        public void Move(Direction direction, Map map) 
        {
            if (!CanMove(direction,map))
            {
                return;
            }
            Point coordDiff = GameMath.DirectionToCoordDiff(direction);

            map.SetObjWithCoordToNull(this.X, this.Y, MapLayer);
            map.SetObjWithCoord(this.X + coordDiff.X, this.Y + coordDiff.Y, this);
            this.X += coordDiff.X;
            this.Y += coordDiff.Y;
        }

    }
}
