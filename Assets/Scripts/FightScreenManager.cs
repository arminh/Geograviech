﻿using UnityEngine;
using UnityEngine.UI;
using Assets;
using Assets.Scripts;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Consumables;

public class FightScreenManager : MonoBehaviour {
    public GameObject buttonPrefab;

    List<Vector3> enemyPositions;
    List<Vector3> playerPositions;
    GameObject buttonPanel;
    public GameObject canvas;
    private static FightScreenManager instance;

	// Use this for initialization
    void Start()
    {
        buttonPanel = Util.getButtonPanel();
        buttonPanel.transform.SetParent(canvas.transform, false);
    }

    public static FightScreenManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<FightScreenManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    void Awake()
    {
        Debug.Log("Awake");
        if (instance == null)
        {
            //If I am the first instance, make me the Singleton
            instance = this;
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != instance)
                Destroy(this.gameObject);
        }
    }

    public void init(int friendCount, int enemyCount)
    {
        playerPositions = new List<Vector3>();
            enemyPositions = new List<Vector3>();

           List<Vector3> positions = Util.getFightScreenPostitions(friendCount,enemyCount);
           foreach (Vector3 pos in positions)
           {
               Debug.Log("pos:" + pos);
           }
           for (int i = 0; i < positions.Count; i++)
           {
               if(i < friendCount)
               {
                   playerPositions.Add(positions[i]);
               }
               else
               {
                   enemyPositions.Add(positions[i]);
               }
           }
    }

    public void setPositions(List<FightCharacter> fighters)
    {
        int playerCount = 0;
        int enemyCount = 0;
        foreach (FightCharacter character in fighters)
        {
            GameObject sprite = character.Sprite;
            if (character.IsEnemy)
            {

                sprite.transform.position = enemyPositions.ElementAt(enemyCount);
                enemyCount++;

            }
            else
            {

                sprite.transform.position = playerPositions.ElementAt(playerCount);
                playerCount++;

            }
        }
    }

    public void showActionMenu()
    {
        clearButtonPanel();
        RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
        Vector2 panelPosition = panelRectTransform.anchoredPosition;
        Vector2 panelSize = panelRectTransform.sizeDelta;

        Vector2 buttonSize = calculateButtonSize(panelSize, 2);
        List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, 2);

        //use item
            GameObject go = (GameObject)Instantiate(buttonPrefab);
            RectTransform buttonRectTransForm = go.transform as RectTransform;
            buttonRectTransForm.anchoredPosition = buttonPositions[0];
            buttonRectTransForm.sizeDelta = buttonSize;

            go.transform.SetParent(buttonPanel.transform, false);
            go.GetComponentInChildren<Text>().text = "Use Item";

           Button b = go.GetComponent<Button>();
           b.onClick.AddListener(() => FightManager.Instance.useItemChosen());

        //attack
           go = (GameObject)Instantiate(buttonPrefab);
           buttonRectTransForm = go.transform as RectTransform;
           buttonRectTransForm.anchoredPosition = buttonPositions[1];
           buttonRectTransForm.sizeDelta = buttonSize;

           go.transform.SetParent(buttonPanel.transform, false);
           go.GetComponentInChildren<Text>().text = "Attack";

           b = go.GetComponent<Button>();
           b.onClick.AddListener(() => FightManager.Instance.attackChosen());


    }

    private void clearButtonPanel()
    {
        foreach (Transform child in buttonPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void showConsumablesMenu(List<IConsumable> items)
    {
        clearButtonPanel();
        RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
        Vector2 panelPosition = panelRectTransform.anchoredPosition;
        Vector2 panelSize = panelRectTransform.sizeDelta;

        Vector2 buttonSize = calculateButtonSize(panelSize, items.Count +1);
        List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, items.Count +1);

        int i = 0;
        foreach (IConsumable item in items)
        {
            GameObject go = (GameObject)Instantiate(buttonPrefab);
            RectTransform buttonRectTransForm = go.transform as RectTransform;
            buttonRectTransForm.anchoredPosition = buttonPositions[i];
            buttonRectTransForm.sizeDelta = buttonSize;

            go.transform.SetParent(buttonPanel.transform, false);
            go.GetComponentInChildren<Text>().text = item.Name + " x" + item.Quantity; 

            Button b = go.GetComponent<Button>();

            IConsumable captured = item;
            b.onClick.AddListener(() => FightManager.Instance.setChosenItem(captured));
            i++;
        }

        //attack
        GameObject gameObject = (GameObject)Instantiate(buttonPrefab);
        RectTransform backButtonRectTransForm = gameObject.transform as RectTransform;
        backButtonRectTransForm.anchoredPosition = buttonPositions[items.Count];
        backButtonRectTransForm.sizeDelta = buttonSize;

        gameObject.transform.SetParent(buttonPanel.transform, false);
        gameObject.GetComponentInChildren<Text>().text = "Back";

        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => FightManager.Instance.backChosen());
    }

    public void showViecherMenu(List<FightCharacter> viecher)
    {
        clearButtonPanel();
        RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
        Vector2 panelPosition = panelRectTransform.anchoredPosition;
        Vector2 panelSize = panelRectTransform.sizeDelta;

        Vector2 buttonSize = calculateButtonSize(panelSize, viecher.Count + 1);
        List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, viecher.Count + 1);

        int i = 0;
        foreach (FightCharacter viech in viecher)
        {
            GameObject go = (GameObject)Instantiate(buttonPrefab);
            RectTransform buttonRectTransForm = go.transform as RectTransform;
            buttonRectTransForm.anchoredPosition = buttonPositions[i];
            buttonRectTransForm.sizeDelta = buttonSize;

            go.transform.SetParent(buttonPanel.transform, false);
            go.GetComponentInChildren<Text>().text = viech.Name;

            Button b = go.GetComponent<Button>();

            FightCharacter captured = viech;
            b.onClick.AddListener(() => FightManager.Instance.setChosenViech(captured));
            i++;
        }

        //attack
        GameObject gameObject = (GameObject)Instantiate(buttonPrefab);
        RectTransform backButtonRectTransForm = gameObject.transform as RectTransform;
        backButtonRectTransForm.anchoredPosition = buttonPositions[viecher.Count];
        backButtonRectTransForm.sizeDelta = buttonSize;

        gameObject.transform.SetParent(buttonPanel.transform, false);
        gameObject.GetComponentInChildren<Text>().text = "Back";

        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => FightManager.Instance.backChosen());
    }

    public void showAttackMenu(List<Attack> attacks)
    {
        clearButtonPanel();
        RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
        Vector2 panelPosition = panelRectTransform.anchoredPosition;
        Vector2 panelSize = panelRectTransform.sizeDelta;

        Vector2 buttonSize = calculateButtonSize(panelSize, attacks.Count);
        List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, attacks.Count);

        int i = 0;
        foreach (Attack attack in attacks)
        {
            GameObject go = (GameObject)Instantiate(buttonPrefab);
            RectTransform buttonRectTransForm = go.transform as RectTransform;
            buttonRectTransForm.anchoredPosition = buttonPositions[i];
            buttonRectTransForm.sizeDelta = buttonSize;

            go.transform.SetParent(buttonPanel.transform, false);
            go.GetComponentInChildren<Text>().text = attack.Name;

            Button b = go.GetComponent<Button>();

            Attack captured = attack;
            b.onClick.AddListener(() => FightManager.Instance.setChosenAttack(attack));
            i++;
        }
    }

    private Vector2 calculateButtonSize(Vector2 panelSize, int buttonCount)
    {
        Vector2 buttonSize = Vector2.zero;
        buttonSize.x = panelSize.x / 2 - panelSize.x * 0.05f;
        switch (buttonCount)
        {
            case 2:
                {

                    buttonSize.y = panelSize.y;
                    break;
                }
            case 3:
            case 4:
                {
                    buttonSize.y = panelSize.y / 2 - panelSize.y * 0.05f;
                    break;
                }
            default:
                {
                    buttonSize.x = panelSize.x;
                    buttonSize.y = panelSize.y / 3;
                    break;
                }
        }
        return buttonSize;
    }

    private List<Vector2> calculateButtonPositions(Vector2 panelPosition, Vector2 panelSize, int buttonCount)
    {
        panelPosition.x = panelPosition.x - panelSize.x / 2;
        panelPosition.y = panelPosition.y - panelSize.y / 2;
        List<Vector2> buttonPositions = new List<Vector2>();
        switch (buttonCount)
        {
            case 2:
                {
                    buttonPositions.Add(panelPosition);

                    float x = panelPosition.x + panelSize.x / 2 + panelSize.x * 0.05f;
                    float y = panelPosition.y;
                    buttonPositions.Add(new Vector2(x, y));
                    break;
                }
            case 3:
                {
                    float x = panelPosition.x;
                    float y = panelPosition.y + panelSize.y / 2 + panelSize.y * 0.05f;
                    buttonPositions.Add(new Vector2(x, y));

                    x = panelPosition.x + panelSize.x / 2 + panelSize.x * 0.05f;
                    buttonPositions.Add(new Vector2(x, y));

                    x = (panelSize.x - calculateButtonSize(panelSize, 3).x) / 2;
                    y = panelPosition.y;
                    buttonPositions.Add(new Vector2(x, y));
                    break;
                }
            case 4:
                {
                    float x = panelPosition.x;
                    float y = panelPosition.y;
                    buttonPositions.Add(new Vector2(x, y));

                    x = panelPosition.x + panelSize.x / 2 + panelSize.x * 0.05f;
                    buttonPositions.Add(new Vector2(x, y));

                    y = panelPosition.y + panelSize.y / 2 + panelSize.y * 0.05f;
                    buttonPositions.Add(new Vector2(x, y));

                    x = panelPosition.x;
                    buttonPositions.Add(new Vector2(x, y));
                    break;
                }
            default:
                {
                    float firstY = panelPosition.y + 2 * panelSize.y / 3;

                    for (int i = 0; i < buttonCount; i++)
                    {
                        buttonPositions.Add(new Vector2(panelPosition.x, firstY - (i * panelSize.y / 3)));
                    }
                    break;
                }
        }
        return buttonPositions;
    }
}