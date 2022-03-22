using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Engine.InventoryObjects;

namespace Roguelike.Engine.Maps
{
    public class Loot
    {
        private List<ItemProbability>[] _scripts;

        public Loot()
        {
            _scripts = new List<ItemProbability>[6];

            List <ItemProbability> script1 = new(); //All weapon
            script1.Add(new ItemProbability(new Axe(), 0.4f));
            script1.Add(new ItemProbability(new Pen(), 0.2f));
            script1.Add(new ItemProbability(new KitchenKnife(), 0.9f));
            script1.Add(new ItemProbability(new Gun(), 0.1f));
            _scripts[1] = script1;

            List<ItemProbability> script2 = new(); //Work items
            script2.Add(new ItemProbability(new Pen(), 0.8f));
            script2.Add(new ItemProbability(new Pen(), 0.8f));
            script2.Add(new ItemProbability(new Pen(), 0.8f));
            script2.Add(new ItemProbability(new Pen(), 0.8f));
            script2.Add(new ItemProbability(new Pen(), 0.8f));
            script2.Add(new ItemProbability(new KitchenKnife(), 0.2f));
            script2.Add(new ItemProbability(new Bandage(), 0.15f));
            script2.Add(new ItemProbability(new LightBulb(), 0.2f));
            _scripts[2] = script2;

            List<ItemProbability> script3 = new(); //Safe-Life Collection
            script3.Add(new ItemProbability(new Axe(), 0.9f));
            script3.Add(new ItemProbability(new Bandage(), 0.9f));
            script3.Add(new ItemProbability(new KitchenKnife(), 1f));
            script3.Add(new ItemProbability(new LightBulb(), 0.9f));
            script3.Add(new ItemProbability(new Gun(), 0.2f));
            _scripts[3] = script3;

            List<ItemProbability> script4 = new(); //Full Random strange thing (for body...) 
            script4.Add(new ItemProbability(new Axe(), 0.1f));
            script4.Add(new ItemProbability(new Bandage(), 0.5f));
            script4.Add(new ItemProbability(new KitchenKnife(), 0.5f));
            script4.Add(new ItemProbability(new LightBulb(), 0.7f));
            script4.Add(new ItemProbability(new Gun(), 0.05f));
            script4.Add(new ItemProbability(new Pen(), 0.5f));
            script4.Add(new ItemProbability(new Pen(), 0.5f));
            _scripts[4] = script4;

            List<ItemProbability> script5 = new(); //Smt for kitchen 
            script5.Add(new ItemProbability(new KitchenKnife(), 0.8f));
            script5.Add(new ItemProbability(new KitchenKnife(), 0.8f));
            script5.Add(new ItemProbability(new KitchenKnife(), 0.8f));
            script5.Add(new ItemProbability(new Eat(), 0.9f));
            script5.Add(new ItemProbability(new Eat(), 0.9f));
            script5.Add(new ItemProbability(new Eat(), 0.9f));
            script5.Add(new ItemProbability(new Eat(), 0.9f));
            script5.Add(new ItemProbability(new Pen(), 0.2f));
            script5.Add(new ItemProbability(new Pen(), 0.2f));
            _scripts[5] = script5;
        }

        public List<InventoryObject> CreateLoot (int numberOfScript)
        {
            List<InventoryObject> objectInventory = new();
            
            foreach (ItemProbability i in _scripts[numberOfScript])
            {
                bool isDropped = i.Chance - (float)GameMath.rand.NextDouble() >= 0f;
                if (isDropped)
                {
                    objectInventory.Add(i.obj);
                }
            }

            return objectInventory;
        }

    }


    public struct ItemProbability
    {
        public InventoryObject obj {get; set;}
        public float Chance {get; set;}

        public ItemProbability(InventoryObject obj, float chance)
        {
            this.obj = obj;
            Chance = chance;
        }
    }
    
    
}
