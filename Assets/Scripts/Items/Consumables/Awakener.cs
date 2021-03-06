﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Effects;
using Assets.Scripts.FightCharacters;

namespace Assets.Scripts.Items.Consumables
{
    class Awakener : Item, IConsumable
    {
        public Awakener()
            : base("Kaffee")
        {
            this.icon = GameManager.Instance.Icons["coffee"];
            this.description = "Wakes up.";
        }
        public bool use(FightCharacter character)
        {
            return character.cureEffect(Effect.EffectType.SLEEP);
        }
    }
}
