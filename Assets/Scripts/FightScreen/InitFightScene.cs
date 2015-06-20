using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

namespace Assets.Scripts.FightScreen
{
    public class InitFightScene : MonoBehaviour
    {
        private Camera mainCam;
        public GameObject background;


        // Use this for initialization
        void Start()
        {
            mainCam = Camera.main;

            if (background != null)
            {
                Util.InitBackground(background, mainCam);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
