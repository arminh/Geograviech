using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using Assets.Scripts.Utils;
using Assets.Scripts.Consumables;
using Assets.Scripts.ArtificialIntelligence;

namespace Assets.Scripts
{
    class FightManager : MonoBehaviour
    {
        private List<FightCharacter> fighters;
        
        private FightPlayer player;
        private FightCharacter enemy;

        private FightCharacter activeFighter;


        private static FightManager instance;

        public List<GameObject> allViecherPefabs;
       

        private bool isTurnFinished;


        //fields for stateMachine
        int state;
        IConsumable chosenItem = null;
        FightCharacter chosenViech = null;
        Attack chosenAttack = null;
        bool isattack = false;
        bool isUseItem = false;

        bool playerHasChoosen = false;

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
            foreach (FightViech viech in player.ActiveViecher)
            {
                addFighter(viech,false);
                friendCount++;
            }

            addFighter(enemy,true);
          if(enemy is FightBoss)
            {
                foreach (FightCharacter viech in ((FightBoss)enemy).ActiveViecher)
                {
                    addFighter(viech,true);
                    enemyCount++;
                }
            }
            orderFighters();


            FightScreenManager.Instance.init(friendCount, enemyCount);


            bool fightFinished = false;

            while (!fightFinished)
            {
                executeTurn();
                
                fightFinished = player.isDead() || enemy.isDead();
            }


        }

        

        private void executeTurn()
        {
            Debug.Log("Execute Turn");
            FightScreenManager.Instance.setPositions(fighters);
            activeFighter = fighters.FirstOrDefault();
            fighters.RemoveAt(0);

            fighters.Add(activeFighter);
            Debug.Log("Active fighter: " + activeFighter.identifier);
            AI.executeTurn(activeFighter);

            if (!activeFighter.IsEnemy)
            {
                state = 0;
                isTurnFinished = false;
                executeFSM();
            }
            
        }

        private void executeFSM()
        {
            while(!isTurnFinished)
            {
                // TODO implement effects
                if (activeFighter == player)
                {
                    switch (state)
                    {
                        case 0:
                            {
                                FightScreenManager.Instance.showActionMenu();
                                playerHasChoosen = false;
                                break;
                            }
                        case 1:
                            {
                                if (isUseItem)
                                {
                                    FightScreenManager.Instance.showConsumablesMenu(player.Items);

                                }
                                else
                                {
                                    List<FightCharacter> viecher = getAttackableEnemies();
                                    FightScreenManager.Instance.showViecherMenu(viecher);
                                }
                                playerHasChoosen = false;
                                break;
                            }
                        case 2:
                            {
                                if (isUseItem)
                                {
                                    List<FightCharacter> viecher = getPlayerViecher(false);
                                    FightScreenManager.Instance.showViecherMenu(viecher);
                                    playerHasChoosen = false;
                                }
                                else
                                {
                                    attackViech(player.ActiveWeapon.Attack, chosenViech);
                                    isTurnFinished = true;
                                }
                                break;
                            }
                        case 3:
                            {
                                if (isUseItem)
                                {
                                    useItem(chosenItem, chosenViech);
                                    isTurnFinished = true;
                                }
                                else
                                {
                                    throw new NotImplementedException("Schould not be reached!");
                                }
                                break;
                            }
                    }
                }
                else
                {
                    switch (state)
                    {
                        case 0:
                            {
                                FightScreenManager.Instance.showAttackMenu(activeFighter.Attacks);
                                playerHasChoosen = false;
                                break;
                            }
                        case 1:
                            {
                                List<FightCharacter> viecher = getAttackableEnemies();
                                FightScreenManager.Instance.showViecherMenu(viecher);
                                playerHasChoosen = false;
                                break;
                            }
                        case 2:
                            {
                                attackViech(chosenAttack, chosenViech);
                                isTurnFinished = true;
                                break;
                            }
                    }
                }

                while (!playerHasChoosen && !isTurnFinished)
                {
                    Thread.Sleep(100);
                } 
            }
        }

        private void useItem(IConsumable choosenItem, FightCharacter choosenViech)
        {
            ItemDto result = player.useItem(choosenItem, choosenViech);
            //TODO log
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

/*        public void attackEnemy(Attack attack)
        {
            clearButtonPanel();
            List<FightCharacter> availableEnemies = getAttackableEnemies();
            if(availableEnemies.Count == 1)
            {
                attackViech(attack, availableEnemies.FirstOrDefault());
				return;
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
        }*/

        public void attackViech(Attack attack, FightCharacter viech)
        {
            Debug.Log("attackViech");
            AttackDto attackResult = viech.getAttacked(attack);
            Debug.Log("attackViech2");
            String message = "";
            if(viech.IsEnemy)
            {
                message += "Enemy " + viech.Name + " "; 
            }else
            {
                if(viech == player)
                {
                    message += "You ";
                }else
                {
                    message += "Your viech " + viech.Name + " ";
                }
            }
            
            message += "take(s) " + attackResult.getInflictedDamage() + " Damage";
               
                if (viech.isDead())
                {
                    message += "and is dead! ";
                    fighters.Remove(viech);
                    if(viech == enemy)
                    {
                        message += "You won the fight!";
                    }
                    if(viech == player)
                    {
                        message += "You lost the fight!";
                    }
                }
            
            //TODO log result
                isTurnFinished = true;
        }

  /*      public void turnFinished()
        {
            isTurnFinished = true;
        }*/

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

    /*    public void ItemUsed(ItemDto itemDto)
        {

        }*/



        public List<FightCharacter> getPlayerViecher(Boolean onlyAttackable)
        {
            List<FightCharacter> viecher = new List<FightCharacter>();

            foreach (FightViech viech in player.ActiveViecher)
            {
                if (!viech.isDead())
                {
                    viecher.Add(viech);
                }
            }
            
            if (viecher.Count == 0 || !onlyAttackable)
            {
                viecher.Add(player);
            }
            return viecher;
        }

        /*
        public FightPlayer getHero()
        {
            return player;
        }

        public FightCharacter getEnemy()
        {
            return enemy;
        }

 */
        public void backChosen()
        {
            state--;
            playerHasChoosen = true;
        }

        public void useItemChosen()
        {
            isUseItem = true;
            isattack = false;
            state++;
            playerHasChoosen = true;
        }

        public void setChosenItem(IConsumable item)
        {
            chosenItem = item;
            playerHasChoosen = true;
        }

        public void attackChosen()
        {
            isUseItem = false;
            isattack = true;
            state++;
            playerHasChoosen = true;
        }

        public void setChosenViech(FightCharacter viech)
        {
            chosenViech = viech;
            state++;
            playerHasChoosen = true;
        }

        public void setChosenAttack(Attack attack)
        {
            chosenAttack = attack;
            state++;
            playerHasChoosen = true;
        }
    }
}
