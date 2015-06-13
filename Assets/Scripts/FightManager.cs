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
using System.Collections;

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
        private bool executeFight = false;


        //fields for stateMachine
        int state;
        IConsumable chosenItem = null;
        FightCharacter chosenViech = null;
        Attack chosenAttack = null;
        bool isattack = false;
        bool isUseItem = false;
        bool isSkip = false;

        bool stateChanged = false;
        Boolean isWaiting = false;
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


            isTurnFinished = true;
            
            executeFight = true;


        }

        public void Update()
        {
            if(player.isDead())
            {
                GameManager.Instance.fightFinished(enemy, player);
                executeFight = false;
            }
            if(enemy.isDead())
            {
                GameManager.Instance.fightFinished(player, enemy);
                executeFight = false;
            }

            if(executeFight && !isWaiting)
            {
                Debug.Log("executeFight");
                StartCoroutine(executeTurn());
            }
        }

        

        private IEnumerator executeTurn()
        {isWaiting = true;
            if (isTurnFinished)
            {
                FightScreenManager.Instance.setPositions(fighters);
                activeFighter = fighters.FirstOrDefault();
                fighters.RemoveAt(0);

                fighters.Add(activeFighter);
                Debug.Log("Active fighter: " + activeFighter.identifier);
                isTurnFinished = false;
                stateChanged = true;
                state = 0;

                if(activeFighter.CurrentEffect != null)
                {

                    
                    yield return StartCoroutine(activeFighter.CurrentEffect.execute(activeFighter));
                    

                    AnimationStatus anim = activeFighter.Sprite.GetComponentInChildren<AnimationStatus>();
                    while (!anim.areSpechialAnimationsFinished())
                    {

                        yield return null;
                    }

                    Log.Instance.print();
                    if(activeFighter.CurrentEffect != null && activeFighter.CurrentEffect is SleepEffect)
                    {
                        isTurnFinished = true;
                        isWaiting = false;
                        yield break;
                    }
                }
            }

            if (!activeFighter.IsEnemy)
            {   
                isTurnFinished = false;
               yield return StartCoroutine(executeFSM());
                
            }else
            {
                AI.executeTurn(activeFighter);
                isTurnFinished = true;
            }
            isWaiting = false;
        }

        private IEnumerator executeFSM()
        {
            Debug.Log("executeFSM");
            if (stateChanged)
            {
                Debug.Log("playerHasChoosen " + state);
                if (activeFighter == player)
                {
                    switch (state)
                    {
                        case 0:
                            {
                                FightScreenManager.Instance.showActionMenu(player.Items.Count != 0,true);
                                stateChanged = false;
                                break;
                            }
                        case 1:
                            {
                                if (isUseItem)
                                {
                                    FightScreenManager.Instance.showConsumablesMenu(player.Items);

                                }
                                else if(isattack)
                                {
                                    List<FightCharacter> viecher = getAttackableEnemies();
                                    FightScreenManager.Instance.showViecherMenu(viecher);
                                }
                                else if (isSkip)
                                {
                                    isTurnFinished = true;
                                }
                                stateChanged = false;
                                break;
                            }
                        case 2:
                            {
                                if (isUseItem)
                                {
                                    List<FightCharacter> viecher = getPlayerViecher(false);
                                    FightScreenManager.Instance.showViecherMenu(viecher);
                                    stateChanged = false;
                                }
                                else
                                {
                                   yield return StartCoroutine(attackViech(player.ActiveWeapon.Attack, chosenViech));
                                    
                                }
                                break;
                            }
                        case 3:
                            {
                                if (isUseItem)
                                {
                                    useItem(chosenItem, chosenViech);
                                }
                                else
                                {
                                    throw new NotImplementedException("Schould not be reached!");
                                }
                                break;
                            }
                        default:
                            {
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
                                FightScreenManager.Instance.showActionMenu(false, false);
                                stateChanged = false;
                                break;
                            }
                        case 1:
                            {
                                if (isattack)
                                {
                                    FightScreenManager.Instance.showAttackMenu(activeFighter.Attacks);
                                }else if(isSkip)
                                {
                                    isTurnFinished = true;
                                }
                                stateChanged = false;
                                break;
                            }
                        case 2:
                            {
                                List<FightCharacter> viecher = getAttackableEnemies();
                                FightScreenManager.Instance.showViecherMenu(viecher);
                                stateChanged = false;
                                break;
                            }
                        case 3:
                            {
                                yield return StartCoroutine(attackViech(chosenAttack, chosenViech));
                                break;
                            }
                    }
                }
            }
            yield break;
        }

        private void useItem(IConsumable choosenItem, FightCharacter choosenViech)
        {
            player.useItem(choosenItem, choosenViech);
            isTurnFinished = true;
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

        public IEnumerator attackViech(Attack attack, FightCharacter viech)
        {
            
            Debug.Log("attackViech");

            //zum gegner fahren
            
            Vector3 position = activeFighter.Sprite.transform.position;
            Vector3 enemyPosition = viech.Sprite.transform.position;
            BoxCollider2D enemyCollider = viech.Sprite.GetComponentInChildren<BoxCollider2D>();
            BoxCollider2D collider = activeFighter.Sprite.GetComponentInChildren<BoxCollider2D>();
            float xPosition;
            if(activeFighter.IsEnemy)
            {
                xPosition = enemyPosition.x + enemyCollider.bounds.size.x / 2 + collider.bounds.size.x / 2;
            }
            xPosition = enemyPosition.x - enemyCollider.bounds.size.x/2 - collider.bounds.size.x/2;
            float yPosition = enemyPosition.y - enemyCollider.bounds.size.y/2 + collider.bounds.size.y/2;
            Vector3 attackPosition = new Vector3(xPosition, yPosition);

            GoToPoint gotoPoint = activeFighter.Sprite.GetComponent<GoToPoint>();
            gotoPoint.start(attackPosition);
            while(!gotoPoint.isFinished())
            {
                yield return null;
            }

            //TODO Attackanimation

            if (activeFighter.CurrentEffect != null && activeFighter.CurrentEffect is StunEffect)
            {
                System.Random rand = new System.Random();
                int action = rand.Next(1, 3);
                Log.Instance.Info(activeFighter.Name + "is confused!");
                switch (action)
                {
                    case 1:
                        viech.getAttacked(attack,activeFighter.Strength);
                        break;
                    case 2:
                        Log.Instance.Info("It hurts itself!");
                        activeFighter.getAttacked(attack, activeFighter.Strength);

                        break;
                    case 3:
                        Log.Instance.Info("It misses the enemy!");
                        break;
                }
            }
            else
            {
                viech.getAttacked(attack, activeFighter.Strength);
            }




            while(!activeFighter.Sprite.GetComponentInChildren<AnimationStatus>().areSpechialAnimationsFinished())
            {
                yield return null;
            }

            while (!viech.Sprite.GetComponentInChildren<AnimationStatus>().areSpechialAnimationsFinished())
            {
                yield return null;
            }

            Log.Instance.print();

            if (viech.isDead())
            {
                fighters.Remove(viech);
                Destroy(viech.Sprite);
            }

            if (activeFighter.isDead())
            {
                fighters.Remove(activeFighter);
                Destroy(activeFighter.Sprite);
            }else
            {
                gotoPoint.start(position);
                while (!gotoPoint.isFinished())
                {
                    yield return null;
                }
            }
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

        public void incrementState()
        {
            state++;
            stateChanged = true;
        }
        public void decrementState()
        {
            state--;
            stateChanged = true;
        }

        public void backChosen()
        {
            decrementState();
            isattack = false;
            isUseItem = false;
            isSkip = false;
        }

        public void useItemChosen()
        {
            Debug.Log("useItemChosen");
            isUseItem = true;
            isattack = false;
            incrementState();
        }

        public void setChosenItem(IConsumable item)
        {
            chosenItem = item;
            Debug.Log("setChosenItem");
            incrementState();
        }

        public void attackChosen()
        {
            Debug.Log("attackChosen");
            isUseItem = false;
            isattack = true;
            incrementState();
        }

        public void setChosenViech(FightCharacter viech)
        {
            chosenViech = viech;
            incrementState();
        }

        public void setChosenAttack(Attack attack)
        {
            chosenAttack = attack;
            incrementState();
        }

        public void skipChosen()
        {
            Debug.Log("attackChosen");
            isSkip = true;
            incrementState();
        }
    }
}
