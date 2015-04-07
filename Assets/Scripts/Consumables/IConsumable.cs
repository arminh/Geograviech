using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    interface IConsumable: Item
    {
        public bool use(Character character);
    }
}
