using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Consumables
{
    public interface IConsumable
    {
        bool use(FightCharacter character);
        String Name{ get; }

        int DropChance{ get; }

        int Quantity{ get; }

        GameObject Icon { get;  }
    }
}
