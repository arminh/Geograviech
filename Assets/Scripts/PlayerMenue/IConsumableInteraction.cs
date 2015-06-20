using Assets.Scripts.Consumables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PlayerMenue
{
    public interface IConsumableInteraction
    {
        void Consume(IConsumable item);
    }
}
