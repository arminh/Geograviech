using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    public interface IConsumable
    {
        bool use(FightCharacter character);
        String Name{ get; }

        String Description { get; }

        int Quantity{ get; }

        Sprite Icon { get; }
    }
}
