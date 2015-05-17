﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
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

        /// <summary>
        /// Calculates the positions of friend and foe.
        /// The length of the List is friendCount + enemyCount, so the first number (friendCount) of Vectors are the positions of the friendly characters, and the rest are enemy positions.
        /// The positions are calculated from BOTTOM TO TOP!! (change possible)
        /// </summary>
        /// <param name="friendCount"></param>
        /// <param name="enemyCount"></param>
        /// <returns></returns>
        public static List<Vector3> getFightScreenPostitions(int friendCount, int enemyCount)
        {
            float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            Vector3 leftDown = Camera.main.transform.position;
            leftDown.x -= worldScreenWidth / 2.0f;
            leftDown.y -= worldScreenHeight / 2.0f;

            Vector3 rightDown = leftDown;
            leftDown.x += worldScreenWidth;

            float spaceBetweenCharactersAndScreen = 5;

            float spaceBetweenFriends = worldScreenHeight / (float)(friendCount + 1);
            float spaceBetweenEnemies = worldScreenHeight / (float)(enemyCount + 1);


            List<Vector3> positions = new List<Vector3>();

            float posX = leftDown.x + spaceBetweenCharactersAndScreen;
            float posY;

            for (int index = 0; index < friendCount; index++)
            {
                posY = leftDown.y + spaceBetweenFriends * (index + 1);
                positions.Add(new Vector3(posX, posY, 0));
            }

            posX = rightDown.x - spaceBetweenCharactersAndScreen;

            for (int index = 0; index < enemyCount; index++)
            {
                posY = rightDown.y + spaceBetweenEnemies * (index + 1);
                positions.Add(new Vector3(posX, posY, 0));
            }

            return positions;
        }
        
        public static GameObject getButtonPanel()
        {
            //TODO gebnerate Panel for buttons;
            return null;
        }
    }
}