using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class ExpandableListScript : MonoBehaviour
{
    public RectTransform ListItem;
    public bool IsExpanded;

    private RectTransform Image;

    void Start()
    {
        ListItem.gameObject.SetActive(IsExpanded);
        List<Image> images = new List<Image>();
        GetComponentsInChildren<Image>(images);
        Image = images.LastOrDefault().rectTransform;
    }

    public void OnListItemClick()
    {
        IsExpanded = !IsExpanded;
        ListItem.gameObject.SetActive(IsExpanded);
        if(IsExpanded)
        {
            Image.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
        }
        else 
        {
            Image.rotation = new Quaternion();
        }
    }
}
