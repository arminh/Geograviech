using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Assets.Scripts
{
    public class GameManager: MonoBehaviour
    {
        private static GameManager instance;

        private Player player;

        private static GameManager gameManager = null;

        public void init()
        {
            //TODO: Read Savefile
            player = new Player(50, 1, 10, "Armin", "Player", 0, 0, new List<Viech>(), new List<Viech>(), new List<Weapon>(), null, new List<IItem>(), new  List<Attack>());
        }

        public void showMenu()
        {
            Application.LoadLevel("PlayerMenue");
        }

        public void startFight(FightCharacter enemy)
        {
            Application.LoadLevel("Fightscreen");

            FightPlayer hero = player.createHero();
            FightManager fightmanager = FindObjectsOfType(typeof(FightManager))[0] as FightManager;
            fightmanager.fight(hero, enemy);

            if (hero.isDead())
            {

            }
            else if(enemy.isDead())
            {
               // player.gainXp(enemy.xp)
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
