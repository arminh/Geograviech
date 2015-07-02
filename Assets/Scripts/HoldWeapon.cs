using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

public class HoldWeapon : MonoBehaviour 
{
    public List<GameObject> weaponPrefabs;

    public void Start()
    {
        var player = GameManager.Instance.getPlayer();
        if (player != null && player.ActiveWeapon != null)
            AddWeapon(player.ActiveWeapon.Icon);
    }

    public void AddWeapon(Sprite weapon) 
    {
        Debug.Log("AddWeapon");
        var query = from pref in weaponPrefabs
                    where pref.GetComponent<SpriteRenderer>().sprite.Equals(weapon) 
                    select pref;
        GameObject Weapon = Instantiate(query.FirstOrDefault());
        if(Weapon)
        {
            RemoveWeapon();
            Debug.Log(Weapon.transform.position);
            Debug.Log(Weapon.transform.localPosition);
            Weapon.transform.SetParent(transform, false);
            var lP = Weapon.transform.localPosition;
            Weapon.transform.localPosition = new Vector3(lP.x, lP.y, 0.0f);
            Debug.Log(Weapon.transform.position);
        }
	}

    public void RemoveWeapon()
    {
        if(transform.childCount > 0)
            foreach (Transform child in transform)
                Destroy(child.gameObject);
    }
}
