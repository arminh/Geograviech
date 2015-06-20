using UnityEngine; using System.Collections;

public class LifeBar : MonoBehaviour {

   // Vector2 pos = new Vector2(25, 40);
 //   Vector2 size = new Vector2(100, 300);
    public Texture2D progressBarFull;
    public Texture2D progressBarEmpty;

    private int health = 50;
    private int maxHealth = 100;
    private bool isEnemy = false;


    public int Health
    {
        get { return maxHealth; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return health; }
        set { maxHealth = value; }
    }

    public bool IsEnemy
    {
        get { return isEnemy; }
        set { isEnemy = value; }
    }


    public void OnGUI()
    {
        Vector3 position = transform.position;
        BoxCollider2D collider = gameObject.GetComponentInChildren<BoxCollider2D>();
        position.y *= -1;
        position.y -= collider.bounds.size.y/4;

        Debug.Log(gameObject.name + " " + transform.localScale);
        if(isEnemy)
        {
            position.x += collider.bounds.size.x / 4;
        }else
        {
            position.x -= collider.bounds.size.x / 2 +1;
        }

        position = Camera.main.WorldToScreenPoint(position);
        
     
        // draw the background:
        GUI.BeginGroup(new Rect(position.x, position.y, progressBarFull.width * 0.5f, progressBarFull.height * 0.35f));
        GUI.Box(new Rect(0, 0, progressBarFull.width * 0.5f, progressBarFull.height * 0.35f), progressBarFull);
      

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, progressBarFull.width * 0.5f, progressBarFull.height * 0.35f * (1.0f - (float)health / (float)maxHealth)));
        GUI.Box(new Rect(0, 0, progressBarFull.width * 0.5f, progressBarFull.height * 0.35f), progressBarEmpty);
        GUI.EndGroup();

        GUI.EndGroup();

    }


}