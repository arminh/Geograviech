using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class Elite: Character
    {
        protected List<Viech> viecher;
        protected List<Viech> activeViecher;

        protected abstract void chooseViech();

	protected List<Item> items;

        public List<Item> getItems()
        {
            return items;
        }

        protected abstract void switchViech();

        public List<Viech> getActiveViecher()
        {
            return activeViecher;
        }

        public override AttackDto getAttacked(Attack attack)
        {

            if (!activeViecher.Any())
            {
                attackResult = new AttackDto();
                attackResult.setAttackedChar(this);
                attackResult.setInflictedDamage(applyDamage(attack.getDamage()));
              //  attackResult.setCurrentEffect(applyEffect(attack.getEffect()));
            }
            else
            {
                //Choose which Viech gets attacked
            }

            return attackResult;
        }



        public override bool isElite()
        {
            return true;
        }
    }
}
