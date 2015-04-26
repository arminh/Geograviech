using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class FightManager
    {
        private List<Character> fighters;
        private static FightManager fightManager;

        private Hero player;

        private Character enemy;

        private FightManager() 
        { 

        }

        private void orderFighters()
        {
            //fighters.Sort((c1, c2) => c1.getSpeed().CompareTo(c2.getSpeed())); so sollte das auch funktioniern (lambda expression)
            fighters.Sort(
                delegate(Character c1, Character c2)
                {
                    return c1.getSpeed().CompareTo(c2.getSpeed());
                }
            );
        }

        public void startFight(Hero player,Character enemy)
        {
            this.player = player;
            this.enemy = enemy;

            fighters = new List<Character>();
            fighters.Add(player);
            foreach (Character viech in player.getActiveViecher())
            {
                fighters.Add(viech);
            }

            fighters.Add(enemy);
            if(enemy.isElite())
            {
                foreach (Character viech in ((Elite)enemy).getActiveViecher())
                {
                    fighters.Add(viech);
                }
            }
            orderFighters();

            executeTurn();
        }

        private void executeTurn()
        {
            Character fighter = fighters.FirstOrDefault();
            fighters.RemoveAt(0);
            fighters.Add(fighter);
            fighter.executeTurn();
        }

        public void addFighter(Character character)
        {
            fighters.Add(character);
        }

        public static FightManager instance()
        {
            if (fightManager == null)
            {
                fightManager = new FightManager();
            }

            return fightManager;
        }

        public void attackEnemy(Attack attack)
        {
            int damage = enemy.getAttacked(attack);
        }
        public void attackPlayer(Attack attack)
        {
            int damage = player.getAttacked(attack);
        }

        public void turnFinished()
        {
            executeTurn();
        }

        public Hero getHero()
        {
            return player;
        }

        public Character getEnemy()
        {
            return enemy;
        }
    }
}
