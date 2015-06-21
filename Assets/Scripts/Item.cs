using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public abstract class Item
    {
        protected string name;
        protected string description;
        protected int dropChance;
        protected int quantity;

        protected Sprite icon;

        public Item()
        {
        }

        public Item(string name, int dropChance, int quantity, Sprite icon)
        {
            this.name = name;
            this.dropChance = dropChance;
            this.quantity = quantity;
            this.icon = icon;
        }

        public String Description
        {
            get { return description; }
        }

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

        public Sprite Icon
        {
            get { return icon; }
        }

    }
}
