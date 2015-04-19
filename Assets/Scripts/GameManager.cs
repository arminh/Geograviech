using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {

        private static GameManager gameManager = null;

        private GameManager()
        {

        }

        public static GameManager instance()
        {
            if (gameManager == null)
            {
                gameManager = new GameManager();
            }

            return gameManager;
        }

        public void createCatchViechUI()
        {

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
