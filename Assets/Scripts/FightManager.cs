using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using Assets.Scripts.Utils;

namespace Assets.Scripts
{
    class FightManager : MonoBehaviour
    {
        private List<Character> fighters;
        private Hero player;

        private Character enemy;

        public List<GameObject> allViecherPefabs;
        public GameObject buttonPrefab;

        List<Vector3> enemyPositions;
        List<Vector3> playerPositions;

        private bool isTurnFinished;

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

            int friendCount = 1;
            int enemyCount = 1;

            fighters = new List<Character>();
            fighters.Add(player);
            foreach (Character viech in player.getActiveViecher())
            {
                addFighter(viech,false);
                friendCount++;
            }

            fighters.Add(enemy);
            if(enemy.isElite())
            {
                foreach (Character viech in ((Elite)enemy).getActiveViecher())
                {
                    addFighter(viech,true);
                    enemyCount++;
                }
            }
            orderFighters();

           List<Vector3> positions = Utils.Utils.getFightScreenPostitions(friendCount, enemyCount);


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
            foreach (GameObject sprite in allViecherPefabs)
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
            List<Character> availableEnemies = getAttackableEnemies();

            GameObject buttonPanel = Utils.Utils.getButtonPanel();
            RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
            Vector2 panelPosition = panelRectTransform.anchoredPosition;
            Vector2 panelSize = panelRectTransform.sizeDelta;

            Vector2 buttonSize = calculateButtonSize(panelSize, availableEnemies.Count);
            List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, availableEnemies.Count);

            for (int i = 0; i < availableEnemies.Count; i++)
            {
                GameObject go = (GameObject)Instantiate(buttonPrefab);
                RectTransform buttonRectTransForm = go.transform as RectTransform;
                buttonRectTransForm.anchoredPosition = buttonPositions[i];
                buttonRectTransForm.sizeDelta = buttonSize;

                go.transform.parent = buttonPanel.transform;
                go.GetComponentInChildren<Text>().text = availableEnemies[i].getName();

                Button b = go.GetComponent<Button>();
                Character captured = availableEnemies[i];
                b.onClick.AddListener(() => attackViech(attack,captured));
            }
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
            GameObject buttonPanel = Utils.Utils.getButtonPanel();
            RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
            Vector2 panelPosition = panelRectTransform.anchoredPosition;
            Vector2 panelSize = panelRectTransform.sizeDelta;

            Vector2 buttonSize = calculateButtonSize(panelSize, labels.Count);
            List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, labels.Count);
            
            for (int i = 0; i < labels.Count; i++)
            {
                GameObject go = (GameObject)Instantiate(buttonPrefab);
                RectTransform buttonRectTransForm = go.transform as RectTransform;
                buttonRectTransForm.anchoredPosition = buttonPositions[i];
                buttonRectTransForm.sizeDelta = buttonSize;

                go.transform.parent = buttonPanel.transform;
                go.GetComponentInChildren<Text>().text = labels[i];

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
                viecher.Add(player);
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

        private Vector2 calculateButtonSize(Vector2 panelSize, int buttonCount)
        {
            Vector2 buttonSize = Vector2.zero;
            buttonSize.x = panelSize.x / 2 - panelSize.x *0.05f;
            switch (buttonCount)
            {
                case 2:
                    {

                        buttonSize.y = panelSize.y;
                        break;
                    }
                case 3:
                case 4:
                    {
                        buttonSize.y = panelSize.y / 2 - panelSize.y * 0.05f;
                        break;
                    }
                default:
                    {
                        buttonSize.x = panelSize.x;
                        buttonSize.y = panelSize.y/3;
                        break;
                    }                
            }
            return buttonSize;
        }

        private List<Vector2> calculateButtonPositions(Vector2 panelPosition, Vector2 panelSize, int buttonCount)
        {
            List<Vector2> buttonPositions = new List<Vector2>();
            switch (buttonCount)
            {
                case 2:
                    {
                        buttonPositions.Add(panelPosition);

                        float x = panelPosition.x + panelSize.x / 2 + panelSize.x * 0.05f;
                        float y = panelPosition.y;
                        buttonPositions.Add(new Vector2(x, y));
                        break;
                    }
                case 3:
                    {
                        float x = panelPosition.x;
                        float y = panelPosition.y + panelSize.y / 2 + panelSize.y * 0.05f;
                        buttonPositions.Add(new Vector2(x, y));

                        x = panelPosition.x + panelSize.x / 2 + panelSize.x * 0.05f;
                        buttonPositions.Add(new Vector2(x, y));

                        x = (panelSize.x - calculateButtonSize(panelSize,3).x) / 2;
                        y = panelPosition.y;
                        buttonPositions.Add(new Vector2(x, y));
                        break;
                    }
                case 4:
                    {
                        float x = panelPosition.x;
                        float y = panelPosition.y;
                        buttonPositions.Add(new Vector2(x, y));

                        x = panelPosition.x + panelSize.x / 2 + panelSize.x * 0.05f;
                        buttonPositions.Add(new Vector2(x, y));

                        y = panelPosition.y + panelSize.y / 2 + panelSize.y * 0.05f;
                        buttonPositions.Add(new Vector2(x, y));

                        x = panelPosition.x;
                        buttonPositions.Add(new Vector2(x, y));
                        break;
                    }
                default:
                    {
                        float firstY = panelPosition.y + 2*panelSize.y/3;

                        for (int i = 0; i < buttonCount; i++)
                        {
                            buttonPositions.Add(new Vector2(panelPosition.x,firstY - (i * panelSize.y/3)));
                        }
                        break;
                    }
            }
            return buttonPositions;
        }
    }
}
