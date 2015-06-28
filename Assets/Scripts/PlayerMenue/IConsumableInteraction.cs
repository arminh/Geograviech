using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Items.Consumables;

namespace Assets.Scripts.PlayerMenue
{
    public interface IConsumableInteraction
    {
        void Consume(IConsumable item);
    }
}
