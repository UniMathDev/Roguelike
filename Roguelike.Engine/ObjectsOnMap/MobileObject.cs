using System.Drawing;
using Roguelike.Engine.Enums;
using System.Collections.Generic;
using Roguelike.Engine.Maps;
using Roguelike.Engine.Monsters;
using System.Linq;

namespace Roguelike.Engine.ObjectsOnMap
{
    public abstract class MobileObject : ObjectOnMap
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Point coordinates
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
        public bool CanMove(Direction direction, Map map)
        {
            Point coordDiff = GameMath.DirectionToCoordDiff(direction);
            Point movingTo = new(this.X + coordDiff.X, this.Y + coordDiff.Y);
            return map.IsPossibleToMove(movingTo.X, movingTo.Y);
        }
        public void MoveBy(int x, int y, Map map) 
        {
            map.SetObjWithCoordToNull(this.X, this.Y, MapLayer);
            map.SetObjWithCoord(this.X + x, this.Y + y, this);
            this.X += x;
            this.Y += y;
        }
    }
}
