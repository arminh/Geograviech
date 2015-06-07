using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public abstract class Item
    {
        protected string name;
        protected int dropChance;
        protected int quantity;

        public String Name
        {
            get { return name; }
        }

        public int DropChance
        {
            get { return dropChance; }
        }

        public int Quantity
        {
            get { return quantity; }
        }
    }
}
