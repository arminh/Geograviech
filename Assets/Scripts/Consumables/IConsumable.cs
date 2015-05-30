using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Consumables
{
    public interface IConsumable
    {
        bool use(FightCharacter character);
    }
}
