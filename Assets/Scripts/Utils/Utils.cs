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

		float scaleHeight = worldScreenHeight / height;
		float scaleWidth = worldScreenWidth / width;

		Vector3 scale = background.localScale;

		if (scaleHeight < scaleWidth)
        {
			scale.x = scaleWidth;
			scale.y = scaleWidth;
        }
        else
        {
			scale.x = scaleHeight;
			scale.y = scaleHeight;
		}

		background.localScale = scale;

    }


    public static List<Vector3> getFightScreenPostitions(int friendCount, int enemyCount)
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 leftDown = Camera.main.transform.position;
        leftDown.x -= worldScreenWidth / 2.0f;
        leftDown.y -= worldScreenHeight / 2.0f;

        Vector3 rightDown = leftDown;
        leftDown.x += worldScreenWidth;

        float spaceBetweenCharactersAndScreen = 10;

        float spacePartOfFriends = worldScreenHeight / (float)friendCount;
        float spacePartOfEnemy = worldScreenHeight / (float)enemyCount;



        return null;
    }
	
}
