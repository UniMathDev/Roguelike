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
        List<ItemProbability>[] Scripts;

        public Loot()
        {
            Scripts = new List<ItemProbability>[6];

            List <ItemProbability> Script1 = new(); //All weapon
            Script1.Add(new ItemProbability(new Axe(), 0.4f));
            Script1.Add(new ItemProbability(new Pen(), 0.2f));
            Script1.Add(new ItemProbability(new KitchenKnife(), 0.9f));
            Script1.Add(new ItemProbability(new Gun(), 0.1f));

            List<ItemProbability> Script2 = new(); //Work items
            Script2.Add(new ItemProbability(new Pen(), 0.8f));
            Script2.Add(new ItemProbability(new Pen(), 0.8f));
            Script2.Add(new ItemProbability(new Pen(), 0.8f));
            Script2.Add(new ItemProbability(new Pen(), 0.8f));
            Script2.Add(new ItemProbability(new Pen(), 0.8f));
            Script2.Add(new ItemProbability(new KitchenKnife(), 0.2f));
            Script2.Add(new ItemProbability(new Bandage(), 0.15f));
            Script2.Add(new ItemProbability(new LightBulb(), 0.2f));

            List<ItemProbability> Script3 = new(); //Safe-Life Collection
            Script3.Add(new ItemProbability(new Axe(), 0.9f));
            Script3.Add(new ItemProbability(new Bandage(), 0.9f));
            Script3.Add(new ItemProbability(new KitchenKnife(), 1f));
            Script3.Add(new ItemProbability(new LightBulb(), 0.9f));
            Script3.Add(new ItemProbability(new Gun(), 0.2f));

            List<ItemProbability> Script4 = new(); //Full Random strange thing (for body...) 
            Script4.Add(new ItemProbability(new Axe(), 0.1f));
            Script4.Add(new ItemProbability(new Bandage(), 0.5f));
            Script4.Add(new ItemProbability(new KitchenKnife(), 0.5f));
            Script4.Add(new ItemProbability(new LightBulb(), 0.7f));
            Script4.Add(new ItemProbability(new Gun(), 0.05f));
            Script4.Add(new ItemProbability(new Pen(), 0.5f));
            Script4.Add(new ItemProbability(new Pen(), 0.5f));

            List<ItemProbability> Script5 = new(); //Smt for kitchen 
            Script5.Add(new ItemProbability(new KitchenKnife(), 0.8f));
            Script5.Add(new ItemProbability(new KitchenKnife(), 0.8f));
            Script5.Add(new ItemProbability(new KitchenKnife(), 0.8f));
            Script5.Add(new ItemProbability(new Eat(), 0.9f));
            Script5.Add(new ItemProbability(new Eat(), 0.9f));
            Script5.Add(new ItemProbability(new Eat(), 0.9f));
            Script5.Add(new ItemProbability(new Eat(), 0.9f));
            Script5.Add(new ItemProbability(new Pen(), 0.2f));
            Script5.Add(new ItemProbability(new Pen(), 0.2f));
        }

        public List<InventoryObject> CreateLoot (int numberOfScript) //Type Object, ???
        {
            List<InventoryObject> ObjectInventory = new();

            foreach (ItemProbability i in Scripts[numberOfScript])
            {
                bool IsDropped = i.Chance - (float)GameMath.rand.NextDouble() >= 0f;
                if (IsDropped)
                {
                    ObjectInventory.Add(i.obj);
                }
            }

            return ObjectInventory;
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
