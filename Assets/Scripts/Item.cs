using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Assets
{
    public abstract class Item
    {
        protected string name;
        protected int dropChance;
        protected int quantity;

        protected Image icon;

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
            set { quantity = value; }
        }

        public Image Icon
        {
            get { return icon; }
        }

    }
}
