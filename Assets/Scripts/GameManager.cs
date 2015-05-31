using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Consumables;
using System.Threading;

namespace Assets.Scripts
{
    public class GameManager: MonoBehaviour
    {
        private static GameManager instance;

        private Player player;

        private static GameManager gameManager = null;

        private bool levelWasLoaded = false;
        private void OnLevelWasLoaded(int iLevel)
        {
            Debug.Log("Level loaded");
            levelWasLoaded = true;
        }

        public void init()
        {
            //TODO: Read Savefile
            Debug.Log("Start");
            Weapon weapon = new Weapon( new Attack ("TestAttack", ElementType.EARTH, 15, new Effect ("TestEffect", Effect.EffectType.POISON, 50, 50)));
            List<Weapon> weapons = new List<Weapon>();
            List<Viech> activeViecher = new List<Viech>();
            List<Viech> viecher = new List<Viech>();
            List<Attack> attacks = new List<Attack>();
            attacks.Add(new Attack("TestAttack", ElementType.EARTH, 15, new Effect("TestEffect", Effect.EffectType.POISON, 50, 50)));

            activeViecher.Add(new Viech(10, 20, 4, "Garganton", "Gargoyles", 3, 500, attacks, ElementType.EARTH));

            player = new Player(15, 15, 5, "TestPlayer", "Player", 500, 5, viecher, activeViecher, weapons, weapon, new List<IConsumable>(), new List<Attack>());

            FightViech enemy = new FightViech("Zerberwelpe", 17, 20, 3, "Skeletor", attacks, ElementType.FIRE, 0.4f, new List<IConsumable>(), 160);

 

                StartCoroutine(executeFight(enemy));
              

        }

        public void showMenu()
        {
            Application.LoadLevel("PlayerMenue");
        }

        public IEnumerator executeFight(FightViech enemy)
        {
            Debug.Log("executeFight");
            Application.LoadLevel("Fightscreen");

          while (!levelWasLoaded)
            {
                Debug.Log("Thread sleeps");
                yield return null;
            }
          levelWasLoaded = false;
          Debug.Log("Thread awoke");
           FightPlayer hero = player.createHero();

            FightManager.Instance.fight(hero, enemy);

            if (hero.isDead())
            {

            }
            else if(enemy.isDead())
            {
                int gainXp = enemy.XpAmount;
                int numChars = player.ActiveViecher.Count + 1;

                int xpPerChar = (int)Mathf.Round((float)gainXp / (float)numChars);
                player.gainXp(xpPerChar);

                foreach(Viech viech in player.ActiveViecher) {
                    viech.gainXp(xpPerChar);
                }


                if(enemy.decideJoin()) {

                    //TODO: Give Viech a name
                    Viech viech = new Viech(enemy.MaxHealth, enemy.Speed, enemy.Strength, "Viech", enemy.Identifier, enemy.Level, 0, enemy.Attacks, enemy.Type);
                    player.addViech(viech);
                }

            }
            else
            {
                //Error: Either hero or enemy should be dead after fight
            }
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<GameManager>();

                    //Tell unity not to destroy this object when loading a new scene!
                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }

        void Awake()
        {
            if (instance == null)
            {
                //If I am the first instance, make me the Singleton
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                //If a Singleton already exists and you find
                //another reference in scene, destroy it!
                if (this != instance)
                    Destroy(this.gameObject);
            }
        }
    }
}
