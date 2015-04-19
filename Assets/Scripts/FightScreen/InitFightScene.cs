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
            initBackgroundPosition();
        }

    }

    private void initBackgroundPosition()
    {
        Vector3 pos = mainCam.transform.position;
        pos.z = 0.0f;
        background.position = pos;

        SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            return;
        }

        float width = spriteRenderer.sprite.bounds.size.x;
        float height = spriteRenderer.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        if (height - worldScreenHeight > width - worldScreenWidth)
        {
            Vector3 scale = background.localScale;
            scale.x = worldScreenWidth / width;
            scale.y = scale.x;
            background.localScale = scale;
        }
        else
        {
            Vector3 scale = background.localScale;
            scale.x = worldScreenHeight / height;
            scale.y = scale.x;
            background.localScale = scale;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
