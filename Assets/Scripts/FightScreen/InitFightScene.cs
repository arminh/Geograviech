using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class InitFightScene : MonoBehaviour
{
    private FightManager fightManager;
    private Camera mainCam;
    public Transform background;


    // Use this for initialization
    void Start()
    {
        fightManager = FightManager.instance();
        mainCam = Camera.main;

        if (background != null)
        {
            Utils.InitBackground(background,mainCam);
        }

        Hero hero = fightManager.getHero();
        Character enemy = fightManager.getEnemy();

        if (hero == null || enemy == null)
        {
            return;
        }

        Vector3 heroPos = mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0));



        //hero.getTransform().position = new Vector3(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
