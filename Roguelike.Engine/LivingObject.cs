using Roguelike.Engine.Enums;
using Roguelike.Engine.Maps;
using Roguelike.Engine.Monsters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Roguelike.Engine
{
    public abstract class LivingObject : IDrawable
    {
        public char Character { get; protected set; }
        public ConsoleColor BackgroundColor { get; protected set; }
        public ConsoleColor ForegroundColor { get; protected set; }

        public int X { get; set; }
        public int Y { get; set; }
        public string Description { get; protected set; }
        public Point coordinates
        {
            get
            {
                return new Point(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public float health { get; protected set; }
        public bool CanMove(Direction direction, Map map, List<LivingObject> livingObjects)
        {
            Point coordDiff = GameMath.DirectionToCoordDiff(direction);
            Point movingTo = new(this.X + coordDiff.X, this.Y + coordDiff.Y);

            foreach (LivingObject lObject in livingObjects)
            {
                if (lObject.coordinates == movingTo)
                {
                    return false;
                }
            }
            return map.IsPossibleToMove(movingTo.X,movingTo.Y);
        }

        //Преобразует monsters и player в один List<LivingObject> и передаёт в функцию выше
        public bool CanMove(Direction direction, Map map, List<Monster> monsters, Player player)
        {
            List<LivingObject> livingObjects = new List<LivingObject>();
            livingObjects.AddRange(monsters.Cast<LivingObject>().ToList());
            livingObjects.Add(player as LivingObject);
            return CanMove(direction, map, livingObjects);
        }

        public bool NextTo(int X, int Y)
        {
            if (Math.Abs(this.X - X) <= 1 && Math.Abs(this.Y - Y) <= 1)
            {
                return true;
            }
            return false;
        }

        public void MoveBy(int X, int Y)
        {
            this.X += X;
            this.Y += Y;
        }

        public void Damage(float amount)
        {
            health -= amount;
        }
    }
}

