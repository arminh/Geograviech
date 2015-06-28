using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class Item
    {
        protected string name;
        protected string description;
        protected int quantity;

        protected Sprite icon;

        public Item()
        {
        }

        public Item(string name, Sprite icon)
        {
            this.name = name;
            this.icon = icon;
            this.quantity = 1;
        }

        public String Description
        {
            get { return description; }
        }

        public String Name
        {
            get { return name; }
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
