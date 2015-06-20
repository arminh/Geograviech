using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.PlayerMenue;
using Assets.Scripts.Consumables;

public class PrefabContainer : MonoBehaviour
{
    public static PrefabContainer manager;

    //public List<GameObject> MonsterPrefabMap;
    //public List<GameObject> MonsterListIconPrefabMap;
    //public List<GameObject> MonsterIconPrefabMap;

    //public List<GameObject> ConsumableListIconPrefabMap;
    //public List<GameObject> ConsumableIconPrefabMap;

    //public List<GameObject> WeaponListIconPrefabMap;
    //public List<GameObject> WeaponIconPrefabMap;

    public GameObject MonsterListIconPrefab;
    public GameObject MonsterIconPrefab;

    public GameObject ConsumableListIconPrefab;
    public GameObject ConsumableIconPrefab;

    public GameObject WeaponListIconPrefab;
    public GameObject WeaponIconPrefab;

    void Awake()
    {
        if (manager != null)
            Destroy(this);
        manager = this;
    }

    public static GameObject getMonsterListIconPrefab()
    {
        return manager.MonsterListIconPrefab;
    }
    public static GameObject getMonsterIconPrefab()
    {
        return manager.MonsterIconPrefab;
    }

    public static GameObject getConsumableListIconPrefab()
    {
        return manager.ConsumableListIconPrefab;
    }

    public static GameObject getConsumableIconPrefab()
    {
        return manager.ConsumableIconPrefab;
    }

    public static GameObject getWeaponListIconPrefab()
    {
        return manager.WeaponListIconPrefab;
    }

    public static GameObject getWeaponIconPrefab()
    {
        return manager.WeaponIconPrefab;
    }

    //public static GameObject getMonsterPrefab(string prefabName)
    //{
    //    var prefab = manager.getPrefabForName(prefabName, manager.MonsterPrefabMap);
    //    return prefab;
    //}

    //public static GameObject getMonsterIconPrefab(string prefabName, bool LargeIcon)
    //{
    //    GameObject prefab;
    //    if(LargeIcon)
    //        prefab = manager.getPrefabForName(prefabName, manager.MonsterListIconPrefabMap);
    //    else
    //        prefab = manager.getPrefabForName(prefabName, manager.MonsterIconPrefabMap);
    //    return prefab;
    //}

    //public static GameObject getConsumableIconPrefab(string prefabName, bool LargeIcon)
    //{
    //    GameObject prefab;
    //    if (LargeIcon)
    //        prefab = manager.getPrefabForName(prefabName, manager.ConsumableListIconPrefabMap);
    //    else
    //        prefab = manager.getPrefabForName(prefabName, manager.ConsumableIconPrefabMap);
    //    return prefab;
    //}

    //public static GameObject getWeaponIconPrefab(string prefabName, bool LargeIcon)
    //{
    //    GameObject prefab;
    //    if (LargeIcon)
    //        prefab = manager.getPrefabForName(prefabName, manager.WeaponListIconPrefabMap);
    //    else
    //        prefab = manager.getPrefabForName(prefabName, manager.WeaponIconPrefabMap);
    //    return prefab;
    //}

    //private GameObject getPrefabForName(string prefabName, List<GameObject> PrefabMap)
    //{
    //    var queary = from prefab in PrefabMap where prefab.name.Contains(prefabName) select prefab;
    //    return queary.FirstOrDefault();
    //}

 
}
