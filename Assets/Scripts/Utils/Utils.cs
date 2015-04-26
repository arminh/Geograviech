using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class Utils
{
    public static void InitBackground(Transform background, Camera mainCam)
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


    public static List<Vector3> getFightScreenPostitions()
    {

        return null;
    }
	
}
