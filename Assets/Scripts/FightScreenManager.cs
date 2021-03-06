﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

using Assets;
using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.FightCharacters;

public class FightScreenManager : MonoBehaviour {
    public GameObject buttonPrefab;
    public Sprite sprite;
    public Sprite attackSprite;
    public Sprite backSprite;
	public Sprite skipTurnSprite;

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

    public void showActionMenu(bool hasItems,bool showItemButton)
    {
        clearButtonPanel();

        int buttonCount = 2;
        if(showItemButton)
        {
            buttonCount = 3;
        }


        RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
        Vector2 panelPosition = panelRectTransform.anchoredPosition;
        Vector2 panelSize = panelRectTransform.sizeDelta;

        Vector2 buttonSize = calculateButtonSize(panelSize, buttonCount);
        List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, buttonCount);

        int currentindex = 0;
        if (showItemButton)
        {
            //use item
            GameObject go = (GameObject)Instantiate(buttonPrefab);
            RectTransform buttonRectTransForm = go.transform as RectTransform;
            buttonRectTransForm.anchoredPosition = buttonPositions[currentindex];
            buttonRectTransForm.sizeDelta = buttonSize;

            go.transform.SetParent(buttonPanel.transform, false);

            Text text = go.GetComponentInChildren<Text>();
            text.text = "Use Item";
            Vector3 position = text.transform.position;
            position.y = position.y - buttonSize.y / 1.7f;
            text.transform.position = position;
            Image image = go.GetComponentInChildren<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;

            Button b = go.GetComponent<Button>();
            b.interactable = hasItems;
            b.onClick.AddListener(() => FightManager.Instance.useItemChosen());
            b.onClick.AddListener(() => clearButtonPanel());
            currentindex++;
        }

        //attack
        GameObject go1 = (GameObject)Instantiate(buttonPrefab);
        RectTransform buttonRectTransForm1 = go1.transform as RectTransform;
           buttonRectTransForm1.anchoredPosition = buttonPositions[currentindex];
           buttonRectTransForm1.sizeDelta = buttonSize;

           go1.transform.SetParent(buttonPanel.transform, false);

           Text text1 = go1.GetComponentInChildren<Text>();
           text1.text = "Attack";
           Vector3 position1 = text1.transform.position;
           position1.y = position1.y - buttonSize.y / 1.7f;
           text1.transform.position = position1;
           Image image1 = go1.GetComponentInChildren<Image>();
           image1.sprite = attackSprite;
           image1.preserveAspect = true;


           Button b1 = go1.GetComponent<Button>();
           b1.onClick.AddListener(() => FightManager.Instance.attackChosen());
           b1.onClick.AddListener(() => clearButtonPanel());
           currentindex++;

         


        //SkipTurn
           GameObject go2 = (GameObject)Instantiate(buttonPrefab);
           RectTransform buttonRectTransForm2 = go2.transform as RectTransform;
           buttonRectTransForm2.anchoredPosition = buttonPositions[currentindex];
           buttonRectTransForm2.sizeDelta = buttonSize;

           go2.transform.SetParent(buttonPanel.transform, false);

           Text text2 = go2.GetComponentInChildren<Text>();
           text2.text = "Skip Turn";
           Vector3 position2 = text2.transform.position;
           position2.y = position2.y - buttonSize.y / 1.7f;
           text2.transform.position = position2;

           Image image2 = go2.GetComponentInChildren<Image>();
			image2.sprite = skipTurnSprite;
           image2.preserveAspect = true;

           Button b2 = go2.GetComponent<Button>();
           b2.onClick.AddListener(() => FightManager.Instance.skipChosen());
           b2.onClick.AddListener(() => clearButtonPanel());
           currentindex++;


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

            Text text = go.GetComponentInChildren<Text>();
            text.text = item.Name + " x" + item.Quantity;

            Vector3 position = text.transform.position;
            position.y = position.y - buttonSize.y / 1.7f;
            text.transform.position = position;

            Image image = go.GetComponentInChildren<Image>();
            image.sprite = item.Icon;
            image.preserveAspect = true;

            Button b = go.GetComponent<Button>();

            IConsumable captured = item;
            b.onClick.AddListener(() => FightManager.Instance.setChosenItem(captured));
            b.onClick.AddListener(() => clearButtonPanel());
            i++;
        }

        //Back
        GameObject gameObject = (GameObject)Instantiate(buttonPrefab);
        RectTransform backButtonRectTransForm = gameObject.transform as RectTransform;
        backButtonRectTransForm.anchoredPosition = buttonPositions[items.Count];
        backButtonRectTransForm.sizeDelta = buttonSize;

        gameObject.transform.SetParent(buttonPanel.transform, false);

        Text text1 = gameObject.GetComponentInChildren<Text>();
        text1.text = "Back";
        Vector3 position1 = text1.transform.position;
        position1.y = position1.y - buttonSize.y / 1.7f;
        text1.transform.position = position1;
        Image image1 = gameObject.GetComponentInChildren<Image>();
        image1.sprite = backSprite;
        image1.preserveAspect = true;

        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => FightManager.Instance.backChosen());
        button.onClick.AddListener(() => clearButtonPanel());
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

            Text text = go.GetComponentInChildren<Text>();
            text.text = viech.Name;
            Vector3 position = text.transform.position;
            position.y = position.y - buttonSize.y / 1.7f;
            text.transform.position = position;
            Image image = go.GetComponentInChildren<Image>();
            image.sprite = viech.Icon;
            image.preserveAspect = true;

            Button b = go.GetComponent<Button>();

            FightCharacter captured = viech;
            b.onClick.AddListener(() => FightManager.Instance.setChosenViech(captured));
            b.onClick.AddListener(() => clearButtonPanel());
            i++;
        }

        //Back
        GameObject gameObject = (GameObject)Instantiate(buttonPrefab);
        RectTransform backButtonRectTransForm = gameObject.transform as RectTransform;
        backButtonRectTransForm.anchoredPosition = buttonPositions[viecher.Count];
        backButtonRectTransForm.sizeDelta = buttonSize;

        gameObject.transform.SetParent(buttonPanel.transform, false);

        Text text1 = gameObject.GetComponentInChildren<Text>();
        text1.text = "Back";
        Vector3 position1 = text1.transform.position;
        position1.y = position1.y - buttonSize.y / 1.7f;
        text1.transform.position = position1;
        Image image1 = gameObject.GetComponentInChildren<Image>();
        image1.sprite = backSprite;
        image1.preserveAspect = true;

        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => FightManager.Instance.backChosen());
        button.onClick.AddListener(() => clearButtonPanel());
    }

    public void showAttackMenu(List<Attack> attacks)
    {
        clearButtonPanel();
        RectTransform panelRectTransform = buttonPanel.transform as RectTransform;
        Vector2 panelPosition = panelRectTransform.anchoredPosition;
        Vector2 panelSize = panelRectTransform.sizeDelta;

        Vector2 buttonSize = calculateButtonSize(panelSize, attacks.Count +1);
        List<Vector2> buttonPositions = calculateButtonPositions(panelPosition, panelSize, attacks.Count +1);

        int i = 0;
        foreach (Attack attack in attacks)
        {
            GameObject go = (GameObject)Instantiate(buttonPrefab);
            RectTransform buttonRectTransForm = go.transform as RectTransform;
            buttonRectTransForm.anchoredPosition = buttonPositions[i];
            buttonRectTransForm.sizeDelta = buttonSize;

            go.transform.SetParent(buttonPanel.transform, false);

            Text text = go.GetComponentInChildren<Text>();
            text.text = attack.Name;
            Vector3 position = text.transform.position;
            position.y = position.y - buttonSize.y / 1.7f;
            text.transform.position = position;
            Image image = go.GetComponentInChildren<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;

            Button b = go.GetComponent<Button>();
            b.interactable = attack.Active;
            Attack captured = new Attack(attack);
			b.onClick.AddListener(() => FightManager.Instance.setChosenAttack(captured));
            b.onClick.AddListener(() => clearButtonPanel());
            i++;
        }

        //Back
        GameObject gameObject = (GameObject)Instantiate(buttonPrefab);
        RectTransform backButtonRectTransForm = gameObject.transform as RectTransform;
        backButtonRectTransForm.anchoredPosition = buttonPositions[attacks.Count];
        backButtonRectTransForm.sizeDelta = buttonSize;

        gameObject.transform.SetParent(buttonPanel.transform, false);

        Text text1 = gameObject.GetComponentInChildren<Text>();
        text1.text = "Back";
        Vector3 position1 = text1.transform.position;
        position1.y = position1.y - buttonSize.y / 1.7f;
        text1.transform.position = position1;
        Image image1 = gameObject.GetComponentInChildren<Image>();
        image1.sprite = backSprite;
        image1.preserveAspect = true;

        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => FightManager.Instance.backChosen());
        button.onClick.AddListener(() => clearButtonPanel());
    }

    private Vector2 calculateButtonSize(Vector2 panelSize, int buttonCount)
    {
        Vector2 buttonSize = Vector2.zero;
        buttonSize.x = panelSize.x / 2.0f - panelSize.x * 0.1f;
        buttonSize.y = panelSize.y / 2.0f - panelSize.y * 0.1f;
        return buttonSize;
    }

    private List<Vector2> calculateButtonPositions(Vector2 panelPosition, Vector2 panelSize, int buttonCount)
    {
        List<Vector2> buttonPositions = new List<Vector2>();
        var buttonSizeHalf = (panelSize.x / 2.0f - panelSize.x * 0.1f) / 2.0f;
        var pseudoMiddleXPos = panelPosition.x - buttonSizeHalf;
        var rowCount = (int)((buttonCount - 1) / 2);
        var yStep = panelSize.y / 2.0f;
        var firstYPos = rowCount * yStep / 2.0f + panelPosition.y - buttonSizeHalf;
         
        for (int i = 0; i < buttonCount; i++)
        {
            float x;

            if (((i+1) % 2) == 1)
	        {
                if ((i+1) == buttonCount)
                {
                    x = pseudoMiddleXPos;
                }
                else
                {
                    x = pseudoMiddleXPos - panelSize.x / 4.0f;
                }
	        }
            else
            {
                x = pseudoMiddleXPos + panelSize.x / 4.0f;
            }

            float y = firstYPos - yStep * (int)(i / 2);

            buttonPositions.Add(new Vector2(x, y));
        }

        return buttonPositions;
    }
}
