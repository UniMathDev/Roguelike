using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Engine.ObjectsOnMap
{
    public interface IUsable : IChangeAble
    {
        public void Use(object useWith);
    }
}
