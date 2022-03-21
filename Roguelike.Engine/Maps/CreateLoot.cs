using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Engine.InventoryObjects;

namespace Roguelike.Engine.Maps
{
    
    public struct Loot
    {
        //скрипт - на вход обьект, на выход список лута
        //функция (обьект, 

        public InventoryObject[] CreateLoot (Type Object, int numberOfScript)
        {
            InventoryObject[] ObjectInventory;
            if (numberOfScript == 1) //Max Weapon
            {
                //bool knockedBack = Chances - (float)GameMath.rand.NextDouble() >= 0f;
                foreach (float i in NumberScript1())
                {

                }
            }
            else if (numberOfScript == 2) //Канцелярия, рабочее место, и т.д.
            {
                (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new Pen());
                (mapCells[y, x].Layers[1] as ISearchable).Inventory.Add(new LightBulb());
            }
            else if (numberOfScript == 3)
            {

            }

        }


        //еще одна структура выпадения вероятности
    }

    public struct Probability
    {

        public static List<float> NumberScript1 ()
        {
            List<float> Chances = new List<float>();
            Chances.Add(0.4f);
            Chances.Add(0.2f);
            Chances.Add(0.9f);
            return Chances;
        }



    }

    
}
