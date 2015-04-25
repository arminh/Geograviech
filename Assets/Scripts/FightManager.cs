using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class FightManager : MonoBehaviour
    {
        private List<Character> fighters;
        private Hero player;

        private Character enemy;

        public List<GameObject> prefabs;

        List<Vector3> enemyPositions;
        List<Vector3> playerPositions;


        public FightManager() 
        { 

        }

        private void orderFighters()
        {
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

            //TODO set EnemyPosition
            //TODO set PlayerPosition

            fighters = new List<Character>();
            fighters.Add(player);
            foreach (Character viech in player.getActiveViecher())
            {
                addFighter(viech,false);
            }

            fighters.Add(enemy);
            if(enemy.isElite())
            {
                foreach (Character viech in ((Elite)enemy).getActiveViecher())
                {
                    addFighter(viech,true);
                }
            }
            orderFighters();

            executeTurn();
        }

        private void setPositions()
        {
            int playerCount = 0;
            int enemyCount = 0;
            foreach (Character character in fighters)
            {
                GameObject sprite = character.getSprite();
                if(character.isEnemy())
                {                   
                   sprite.transform.position = enemyPositions.ElementAt(enemyCount);
                   enemyCount++;
                }else
                {
                    sprite.transform.position = playerPositions.ElementAt(playerCount);
                    playerCount++;
                }
            }
        }

        private void executeTurn()
        {
            setPositions();
            Character fighter = fighters.FirstOrDefault();
            fighters.RemoveAt(0);
            fighters.Add(fighter);  
            fighter.executeTurn();
        }

        private void addFighter(Character character, bool isEnemy)
        {
            character.setIsEnemy(isEnemy);
            fighters.Add(character);
            foreach (GameObject sprite in prefabs)
	        {
                if (sprite.name.Equals(character.getName()))
                {
                    GameObject spriteInitialisation = Instantiate(sprite, Vector3.zero, Quaternion.identity) as GameObject;
                    if(isEnemy)
                    {
                        Vector3 scale = spriteInitialisation.transform.localScale;
                        scale.x *= -1;
                        spriteInitialisation.transform.localScale = scale;
                    }
                    character.setSprite(spriteInitialisation);
                    return;
                }
	        }
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
    }
}
