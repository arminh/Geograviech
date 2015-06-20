using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.PlayerMenue;
using Assets.Scripts.Consumables;

public class MonsterPanelInteractionManager : MonoBehaviour, IConsumableInteraction
{
    public static MonsterPanelInteractionManager manager;

    public RectTransform MonsterPanel;
    public Text Name;
    public Text Type;
    public Text Health;
    public Text LevelXp;
    public Text Damage;
    public Text Speed;
    public Text Attacks;

    private Viech Monster;
    private Hashtable OriginalText;

    void Awake()
    {
        if (manager != null)
            Destroy(this);
        manager = this;
    }

    void Start()
    {
        OriginalText.Add(Name ,Name.text);
        OriginalText.Add(Type, Type.text);
        OriginalText.Add(Health, Health.text);
        OriginalText.Add(LevelXp, LevelXp.text);
        OriginalText.Add(Damage, Damage.text);
        OriginalText.Add(Speed, Speed.text);
        OriginalText.Add(Attacks, Attacks.text);
    }

    public void SetMonsterInfos(Viech monster)
    {
        Monster = monster;
        var monsterImage = monster.Sprite;
        if(monsterImage != null)
        {
            var monsterView = Instantiate(monsterImage);
            monsterView.transform.localPosition = new Vector3(0, 0, -50);
            monsterView.transform.SetParent(MonsterPanel);   
        }
        Name.text = string.Format(Name.text, Monster.Name);
        Type.text = string.Format(Type.text, Monster.Type.ToString());
        Health.text = string.Format(Health.text, Monster.MaxHealth);
        LevelXp.text = string.Format(LevelXp.text, Monster.Level, Monster.Xp);
        Damage.text = string.Format(Damage.text, Monster.Strength);
        Speed.text = string.Format(Speed.text, Monster.Speed);
        Monster.Attacks.ForEach(a => string.Format(Attacks.text, a.Name, ", {0}{1}"));
        Attacks.text = string.Format(Attacks.text, "", "");
    }

    public void ResetMonsterPanel()
    {
        MonsterPanel.DetachChildren();
        Monster = null;

        Name.text = OriginalText[Name] as string;
        Type.text = OriginalText[Type] as string;
        Health.text = OriginalText[Health] as string;
        LevelXp.text = OriginalText[LevelXp] as string;
        Damage.text = OriginalText[Damage] as string;
        Speed.text = OriginalText[Speed] as string;
        Attacks.text = OriginalText[Attacks] as string;
    }

    public static void OnBackToPlayerMenueClick()
    {
        PlayerMenueManager.SwitchMonsterPlayerPanel();
    }

    public static void OnFreeMonsterCkilck()
    {
        PlayerMenueManager.SetMonsterFree(manager.Monster);
    }

    public void Consume(IConsumable item)
    {
        throw new System.NotImplementedException();
    }
}
