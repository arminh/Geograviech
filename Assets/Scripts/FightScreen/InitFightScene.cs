using UnityEngine;
using System.Collections;
using Assets.Scripts;

namespace Assets.Scripts.Utils
{
    public class InitFightScene : MonoBehaviour
    {
        private Camera mainCam;
        public Transform background;


        // Use this for initialization
        void Start()
        {
            mainCam = Camera.main;

            if (background != null)
            {
                Utils.InitBackground(background, mainCam);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
