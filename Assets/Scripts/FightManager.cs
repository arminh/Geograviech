using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using Assets.Scripts.Utils;
using Assets.Scripts.Consumables;

namespace Assets.Scripts
{
    class FightManager : MonoBehaviour
    {
        private List<FightCharacter> fighters;
        private FightPlayer player;
        private FightCharacter activeFighter;

        private FightCharacter enemy;
        public GameObject canvas;

        private static FightManager instance;

        public List<GameObject> allViecherPefabs;
        public GameObject buttonPrefab;

        List<Vector3> enemyPositions;
        List<Vector3> playerPositions;
        GameObject buttonPanel;

        private bool isTurnFinished;

        public static FightManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<FightManager>();

                    //Tell unity not to destroy this object when loading a new scene!
                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }

        void Awake()
        {
            Debug.Log("Awake");
            if (instance == null)
            {
                //If I am the first instance, make me the Singleton
                instance = this;
            }
            else
            {
                //If a Singleton already exists and you find
                //another reference in scene, destroy it!
                if (this != instance)
                    Destroy(this.gameObject);
            }
        }

        void Start()
        {
             buttonPanel = Utils.Utils.getButtonPanel();
             buttonPanel.transform.SetParent(canvas.transform, false);
        }

        public void StartFight()
        {
            Debug.Log("Start");
            Weapon weapon = new Weapon();
            List<FightViech> activeViecher = new List<FightViech>();
            List<Attack> attacks = new List<Attack>();
            attacks.Add(new Attack("TestAttack",ElementType.EARTH,15,new Effect("TestEffect",Effect.EffectType.POISON,50,50)));

            activeViecher.Add(new FightViech("Gargoyles", 10, 20, 4, "Franz", attacks, ElementType.EARTH, 0.5f, new List<IConsumable>(), 150));
            FightPlayer player_ = new FightPlayer(15, 15, 5, "Jürgen", activeViecher, weapon, new List<Attack>(), new List<IConsumable>());

            FightViech enemy_ = new FightViech("Zerberwelpe", 17, 20, 3, "Skeletor", attacks, ElementType.FIRE, 0.4f, new List<IConsumable>(), 160);
            Debug.Log("Start Fight");
            this.fight(player_, enemy_);
            Debug.Log("Done");
        }

        private void orderFighters()
        {
            fighters.Sort(
                delegate(FightCharacter c1, FightCharacter c2)
                {
                    return c1.Speed.CompareTo(c2.Speed);
                }
            );
        }

        public void fight(FightPlayer player, FightCharacter enemy)
        {
            this.player = player;
            this.enemy = enemy;

            int friendCount = 1;
            int enemyCount = 1;
            
            fighters = new List<FightCharacter>();
            addFighter(player,false);
            //fighters.Add(player);
            foreach (FightViech viech in player.ActiveViecher)
            {
                addFighter(viech,false);
                friendCount++;
            }

            addFighter(enemy,true);
            //fighters.Add(enemy);
          if(enemy is FightBoss)
            {
                foreach (FightCharacter viech in ((FightBoss)enemy).ActiveViecher)
                {
                    addFighter(viech,true);
                    enemyCount++;
                }
            }
            orderFighters();

            playerPositions = new List<Vector3>();
            enemyPositions = new List<Vector3>();

           List<Vector3> positions = Utils.Utils.getFightScreenPostitions(friendCount, enemyCount);
           foreach (Vector3 pos in positions)
           {
               Debug.Log("pos:" + pos);
           }
           for (int i = 0; i < positions.Count; i++)
           {
               if(i < friendCount)
               {
                   playerPositions.Add(positions[i]);
               }
               else
               {
                   enemyPositions.Add(positions[i]);
               }
           }


            bool fightFinished = false;

            while (!fightFinished)
            {
                executeTurn();

                //TODo remove 
                fightFinished = true;
                //fightFinished = player.isDead() || enemy.isDead();
            }


        }

        private void setPositions()
        {
            int playerCount = 0;
            int enemyCount = 0;
            foreach (FightCharacter character in fighters)
            {
                GameObject sprite = character.Sprite;
                if(character.IsEnemy)
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
            Debug.Log("Execute Turn");
            setPositions();
            activeFighter = fighters.FirstOrDefault();
            fighters.RemoveAt(0);

            fighters.Add(activeFighter);
            Debug.Log("Active fighter: " + activeFighter.identifier);
            activeFighter.executeTurn();

          /*  if (!activeFighter.IsEnemy)
            {
                isTurnFinished = false;
                while(!isTurnFinished)
                {
                    Thread.Sleep(100);
                }
            }*/
            
        }

        private void addFighter(FightCharacter character, bool isEnemy)
        {

            character.IsEnemy = isEnemy;
            fighters.Add(character);
            foreach (GameObject sprite in allViecherPefabs)
            {

                if (sprite.name.Equals(character.Identifier))
                {
                    
                    GameObject spriteInitialisation = Instantiate(sprite, Vector3.zero, Quaternion.identity) as GameObject;
                    Vector3 scale = spriteInitialisation.transform.localScale;
                    scale.x *= 2;
                    scale.y *= 2;
                    if(isEnemy)
                    {
                       
                        scale.x *= -1;
                        
                        
                    }
                    spriteInitialisation.transform.localScale = scale;
                    character.Sprite = spriteInitialisation;
                    return;
                }
	        }
        }

        public void attackEnemy(Attack attack)
        {
            clearButtonPanel();
            List<FightCharacter> availableEnemies = getAttackableEnemies();
            if(availableEnemies.Count == 1)
            {
                attackViech(attack, availableEnemies.FirstOrDefault());
            }

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

                go.transform.SetParent(buttonPanel.transform,false);
                go.GetComponentInChildren<Text>().text = availableEnemies[i].Identifier;

                Button b = go.GetComponent<Button>();
                FightCharacter captured = availableEnemies[i];
                b.onClick.AddListener(() => attackViech(attack,captured));
            }
        }

        public void attackViech(Attack attack, FightCharacter viech)
        {
            AttackDto attackResult = viech.getAttacked(attack);
            //TODO log result
        }

        public void turnFinished()
        {
            isTurnFinished = true;
        }

        public List<FightCharacter> getAttackableEnemies()
        {
            List<FightCharacter> enemies = new List<FightCharacter>();

             if (enemy is FightBoss)
            {
                foreach (FightCharacter viech in ((FightBoss)enemy).ActiveViecher)
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

        public void showActionMenu(Dictionary<String, Action> actions)
        {
            clearButtonPanel();
            RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
            Vector2 panelPosition = panelRectTransform.anchoredPosition;
            Vector2 panelSize = panelRectTransform.sizeDelta;

            Vector2 buttonSize = calculateButtonSize(panelSize, actions.Count);
            List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize,actions.Count);

            int i = 0;
            foreach(KeyValuePair<String, Action> entry in actions)
            {
                GameObject go = (GameObject)Instantiate(buttonPrefab);
                RectTransform buttonRectTransForm = go.transform as RectTransform;
                buttonRectTransForm.anchoredPosition = buttonPositions[i];
                buttonRectTransForm.sizeDelta = buttonSize;

                go.transform.SetParent(buttonPanel.transform,false);
                go.GetComponentInChildren<Text>().text = entry.Key;
                

                Button b = go.GetComponent<Button>();
                b.onClick.AddListener(() => entry.Value.Invoke());
                i++;
            }
        }

        private void clearButtonPanel()
        {
            foreach (Transform child in buttonPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
        public void showSelectionMenu(Action<String> function, Dictionary<String,int> items)
        {
            clearButtonPanel();
            RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
            Vector2 panelPosition = panelRectTransform.anchoredPosition;
            Vector2 panelSize = panelRectTransform.sizeDelta;

            Vector2 buttonSize = calculateButtonSize(panelSize, items.Count);
            List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, items.Count);

            int i = 0;
            foreach (KeyValuePair<String, int> entry in items)
            {
                GameObject go = (GameObject)Instantiate(buttonPrefab);
                RectTransform buttonRectTransForm = go.transform as RectTransform;
                buttonRectTransForm.anchoredPosition = buttonPositions[i];
                buttonRectTransForm.sizeDelta = buttonSize;

                go.transform.SetParent(buttonPanel.transform, false);
                go.GetComponentInChildren<Text>().text = entry.Key + " : " + entry.Value;

                Button b = go.GetComponent<Button>();
                b.onClick.AddListener(() => function.Invoke(entry.Key));
                i++;
            }
        }

        public List<FightCharacter> getAttackablePlayerViecher()
        {
            List<FightCharacter> viecher = new List<FightCharacter>();

            foreach (FightViech viech in player.ActiveViecher)
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

        public FightPlayer getHero()
        {
            return player;
        }

        public FightCharacter getEnemy()
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
            panelPosition.x = panelPosition.x - panelSize.x / 2;
            panelPosition.y = panelPosition.y - panelSize.y / 2;
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
