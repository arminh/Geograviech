using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

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

        private bool isTurnFinished;


        public FightManager() 
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

        public void fight(Hero player,Character enemy)
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

            bool fightFinished = false;

            while (!fightFinished)
            {
                executeTurn();

                fightFinished = player.isDead() || enemy.isDead();
            }
           
            
        }

        private void setPositions()
        {
            int playerCount = 0;
            int enemyCount = 0;
            foreach (Character character in fighters)
            {
                GameObject sprite = character.getSprite();
                if(character.getIsEnemy())
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

            if(!fighter.getIsEnemy())
            {
                isTurnFinished = false;
                while(!isTurnFinished)
                {
                    Thread.Sleep(100);
                }
            }
            
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
            // TODO ask player which enemy should be attacked
            List<Character> availableEnemies = getAttackableEnemies();
            foreach (Character enemyViech in availableEnemies)
            {
                GameObject go = null;//(GameObject)Instantiate(buttons);
                
          //      go.transform.parent = panel.transform;
                go.GetComponentInChildren<Text>().text = enemyViech.getName();

                
                go.transform.localScale = new Vector3(1, 1, 1);
                Button b = go.GetComponent<Button>();
                Character captured = enemyViech;
                b.onClick.AddListener(() => attackViech(attack,captured));
            }

            AttackDto attackResult = enemy.getAttacked(attack);
        }

        public void attackViech(Attack attack, Character viech)
        {
            AttackDto attackResult = viech.getAttacked(attack);
            //TODO log result
        }

        public void turnFinished()
        {
            isTurnFinished = true;
        }
         
        public List<Character> getAttackableEnemies()
        {
            List<Character> enemies = new List<Character>();
            if (enemy.isElite())
            {
                foreach (Character viech in ((Elite)enemy).getActiveViecher())
                {
                    if(!viech.isDead())
                    {
                        enemies.Add(viech);
                    }
                }
            }
            if(enemies.Count == 0)
            {
                enemies.Add(enemy);
            }
            return enemies;
        }

        public void showMenu(List<String> labels, List<Action> functions)
        {
            for (int i = 0; i < labels.Count; i++)
            {
                GameObject go = null;//(GameObject)Instantiate(buttons);

                //      go.transform.parent = panel.transform;
                go.GetComponentInChildren<Text>().text = labels[i];


                go.transform.localScale = new Vector3(1, 1, 1);
                Button b = go.GetComponent<Button>();
                b.onClick.AddListener(() => functions[i].Invoke());
            }
        }

        public List<Character> getAttackablePlayerViecher()
        {
            List<Character> viecher = new List<Character>();
          
            foreach (Character viech in player.getActiveViecher())
            {
                if (!viech.isDead())
                {
                    viecher.Add(viech);
                }
            }
            
            if (viecher.Count == 0)
            {
                viecher.Add(enemy);
            }
            return viecher;
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
