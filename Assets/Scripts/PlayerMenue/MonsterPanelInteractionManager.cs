using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

using Assets.Scripts;
using Assets.Scripts.PlayerMenue;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.Character;

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
        OriginalText = new Hashtable();
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
        Debug.Log("SetMonsterInfos");
        Monster = monster;
        var monsterImage = monster.Sprite;
        if(monsterImage != null)
        {
            var monsterView = Instantiate(monsterImage);
            var size = getPrefabSize(monsterView.transform);
            Debug.Log("Monstergröße");
            Debug.Log(size);
            monsterView.transform.SetParent(MonsterPanel);
            monsterView.transform.position = new Vector3(0, 0, 0);
            monsterView.transform.localPosition = new Vector3(0, 0, -80);
            var s = monsterView.transform.localScale;
            monsterView.transform.localScale = new Vector3(s.x * 30, s.y * 30, 1);
        }
        Name.text = string.Format(Name.text, Monster.Name);
        Type.text = string.Format(Type.text, Monster.Type.ToString());
        Health.text = string.Format(Health.text, Monster.MaxHealth);
        LevelXp.text = string.Format(LevelXp.text, Monster.Level, Monster.Xp);
        Damage.text = string.Format(Damage.text, Monster.Strength);
        Speed.text = string.Format(Speed.text, Monster.Speed);
        Debug.Log(Attacks.text);
        Monster.Attacks.ForEach(a => { string.Format(Attacks.text, a.Name, ", {0}{1}"); Debug.Log(Attacks.text); });
        Debug.Log(Attacks.text);
        Attacks.text = string.Format(Attacks.text, "", "");
        Debug.Log(Attacks.text);
    }

    public void ResetMonsterPanel()
    {
        if(MonsterPanel.childCount > 0)
            Destroy(MonsterPanel.GetChild(0).gameObject);
        Monster = null;

        Name.text = OriginalText[Name] as string;
        Type.text = OriginalText[Type] as string;
        Health.text = OriginalText[Health] as string;
        LevelXp.text = OriginalText[LevelXp] as string;
        Damage.text = OriginalText[Damage] as string;
        Speed.text = OriginalText[Speed] as string;
        Attacks.text = OriginalText[Attacks] as string;
    }

    public void OnBackToPlayerMenueClick()
    {
        PlayerMenueManager.SwitchToPlayerPanel();
    }

    public void OnFreeMonsterCkilck()
    {
        PlayerMenueManager.SetMonsterFree(manager.Monster);
    }

    public void Consume(IConsumable item)
    {
        throw new System.NotImplementedException();
    }

    private Vector2 getPrefabSize(Transform monster)
    {
        List<SpriteRenderer> renderers = new List<SpriteRenderer>();
        renderers.AddRange(monster.GetComponentsInChildren<SpriteRenderer>());
        renderers.AddRange(monster.GetComponents<SpriteRenderer>());
        var first = renderers.FirstOrDefault();
        if(first != null)
        {
            Vector3 max = first.bounds.max;
            Vector3 min = first.bounds.min;
            foreach (var item in renderers)
            {
                max = Vector3.Max(max, item.bounds.max);
                min = Vector3.Max(min, item.bounds.min);
            }
            return new Vector2(max.x - min.x, max.y - min.y);
        }
        return new Vector2();
    }
}
