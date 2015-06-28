﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

using Assets.Scripts.Utils;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.Character;
using Assets.Scripts.FightCharacters;
using Assets.Scripts.Effects;


namespace Assets.Scripts
{
    public class GameManager: MonoBehaviour
    {
        private static GameManager instance;

        private Player player;
		private FightCharacter enemy; 

        private static GameManager gameManager = null;

        public List<GameObject> allCharactersPefabs;
        private Dictionary<string, GameObject> prefabs;

        public List<Sprite> allCharactersIcons;
        private Dictionary<string, Sprite> icons;

        private bool levelWasLoaded = false;
        private void OnLevelWasLoaded(int iLevel)
        {
			Log.Instance.Info("Level loaded");
            levelWasLoaded = true;

			if (iLevel == 1) 
			{
				Log.Instance.Info("Thread awoke");
				FightPlayer hero = player.createHero();
				
				FightManager.Instance.fight(hero, enemy);
			}
        }

        public void init()
        {
            //TODO: Read Savefile
            prefabs = new Dictionary<string, GameObject>();
            foreach (GameObject sprite in allCharactersPefabs)
            {
                prefabs.Add(sprite.name, sprite);
            }

            icons = new Dictionary<string, Sprite>();
            foreach (Sprite icon in allCharactersIcons)
            {
                icons.Add(icon.name, icon);
            }

            Debug.Log("Start");
            
            //List<Viech> activeViecher = new List<Viech>();
            //List<Viech> viecher = new List<Viech>();
            //List<Attack> attacks0 = new List<Attack>();
            //List<Attack> attacks1 = new List<Attack>();
            //List<Attack> attacks2 = new List<Attack>();

            //attacks1.Add(new Attack("Scream", Enums.ElementType.NORMAL, 3, 5, 7, new StunEffect(100), null));
            //attacks1.Add(new Attack("Poison seeds", Enums.ElementType.EARTH, 2, 4, 6, new PoisonEffect(100), null));
            //attacks2.Add(new Attack("TestAttack", Enums.ElementType.EARTH, 15, 20, 3, new BurnEffect(100), null));
            //attacks2.Add(new Attack("Sleep", Enums.ElementType.EARTH, 15, 20, 3, new SleepEffect(100), null));



            //player = new Player(15, 15, 15, 5, "TestPlayer", 500, 5, new List<Viech>(), new List<Viech>(), new List<Weapon>(), null, new List<IConsumable>(), new List<Attack>(), "Player", "PlayerIcon");

            //Weapon weapon = new Weapon("IceSword", new Attack("TestAttack", Enums.ElementType.EARTH, 6, 8, 0, new FreezeEffect(80), null, player.Level), icons["normalAttack"]);

            //player.addActiveViech(new Viech(100, 100, 20, 4, "Garganton", 3, 500, attacks1, Enums.ElementType.EARTH, "Gargoyles", "GargoyleIcon"));

            //player.addViech(new Viech(10, 10, 20, 4, "Wurzelgemüse", 3, 500, attacks0, Enums.ElementType.EARTH, "Alraune", "AlrauneIcon"));

            //FightViech enemy = new FightViech(170, 20, 3, "Skeletor", attacks2, Enums.ElementType.FIRE, 40, new List<Item>(), 160, "Zerberwelpe", "ZerberwelpeIcon");

            //StartCoroutine(executeFight(enemy));

            Dictionary<int, Viech> activeViecher = new Dictionary<int, Viech>();

            List<Attack> attacks0 = new List<Attack>();
            List<Attack> attacks1 = new List<Attack>();
            List<Attack> attacks2 = new List<Attack>();

            attacks1.Add(new Attack("Scream", Enums.ElementType.NORMAL, 3, 5, 7, new StunEffect(100), null));
            attacks1.Add(new Attack("Poison seeds", Enums.ElementType.EARTH, 2, 4, 6, new PoisonEffect(100), null));
            attacks2.Add(new Attack("TestAttack", Enums.ElementType.EARTH, 15, 20, 3, new BurnEffect(100), null));
            attacks2.Add(new Attack("Sleep", Enums.ElementType.EARTH, 15, 20, 3, new SleepEffect(100), null));

            activeViecher.Add(0, new Viech(10, 10, 20, 4, "Garganton", 3, 500, attacks1, Enums.ElementType.EARTH, "Gargoyles", "GargoyleIcon"));
            activeViecher.Add(2, new Viech(10, 10, 20, 4, "Nervenzwerg", 3, 500, attacks2, Enums.ElementType.WIND, "Imp", "ImpIcon"));

            ItemFactory createItems = new ItemFactory();
            List<Item> droppedItems = createItems.createRandomDrops(1, 10);

            var weapon = createItems.createRandomWeapon(1);
            player = new Player(15, 15, 15, 5, "TestPlayer", 500, 5, new List<Viech>(), activeViecher, new List<Weapon>(), weapon, new List<IConsumable>(), new List<Attack>(), "Player", "PlayerIcon");
            player.addViech(new Viech(10, 10, 20, 4, "Wurzelgemüse", 3, 500, attacks0, Enums.ElementType.EARTH, "Alraune", "AlrauneIcon"));
            player.addViech(new Viech(10, 10, 20, 4, "Wurzelgemüse", 3, 500, attacks0, Enums.ElementType.NORMAL, "SteirischerPanther", "PantherIcon"));
            player.addViech(new Viech(10, 10, 20, 4, "Wurzelgemüse", 3, 500, attacks0, Enums.ElementType.WATER, "GefrorenePelikane", "PelikaneIcon"));
            player.addViech(new Viech(10, 10, 20, 4, "Wurzelgemüse", 3, 500, attacks0, Enums.ElementType.WATER, "Siren", "SirenIcon"));
            player.addViech(new Viech(10, 10, 20, 4, "Wurzelgemüse", 3, 500, attacks0, Enums.ElementType.FIRE, "Zerberwelpe", "ZerberwelpeIcon"));

            foreach (Item item in droppedItems)
            {
                if (item is IConsumable)
                {
                    player.addConsumable(item);
                }
                else
                {
                    player.addWeapon((Weapon)item);
                }
            }
        }

        public Player getPlayer()
        {
            return player;
        }

        public void showMenu()
        {
            Application.LoadLevel("PlayerMenue");
        }

		public void showWorldMap()
		{
			Application.LoadLevel("WorldMap");
		}


        public void executeFight(FightViech enemy)
        {
			Log.Instance.Info("executeFight");
			this.enemy = enemy;
            Application.LoadLevel("Fightscreen");

            /*while (!levelWasLoaded)
            {
				Log.Instance.Info("Thread sleeps");
                yield return null;
            }
            levelWasLoaded = false;
			Log.Instance.Info("Thread awoke");
            FightPlayer hero = player.createHero();

            FightManager.Instance.fight(hero, enemy);*/
        }

        public void fightFinished(FightCharacter winner, FightCharacter looser)
        {
            Debug.Log("fightFinished");
            if (winner.IsEnemy)
            {
                //player.looseFight()
            }
            else
            {
                FightViech enemy = (FightViech)looser;

                int gainXp = enemy.XpAmount;
                int numChars = player.ActiveViecher.Count + 1;

                int xpPerChar = (int)Mathf.Round((float)gainXp / (float)numChars);
                player.gainXp(xpPerChar);

                var query = from slot in player.ActiveViecher select slot.Value;
                foreach (Viech viech in query)
                {
                    viech.gainXp(xpPerChar);
                }

                if (enemy.decideJoin())
                {
                    //TODO: Give Viech a name
                    Viech viech = new Viech(enemy.MaxHealth, enemy.Health, enemy.Speed, enemy.Strength, "Viech", enemy.Level, 0, enemy.Attacks, enemy.Type, enemy.PrefabId, enemy.IconId);
                    player.addViech(viech);
                }

                List<Item> droppedItems = enemy.dropItems();

                foreach (Item item in droppedItems)
                {
                    if (item is IConsumable)
                    {
                        player.addConsumable(item);
                    }
                    else
                    {
                        player.addWeapon((Weapon)item);
                    }
                }
            }

            Application.LoadLevel("MainScreen");
        }

        public Dictionary<string, GameObject> Prefabs
        {
            get { return prefabs; }
        }

        public Dictionary<string, Sprite> Icons
        {
            get { return icons; }
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

