using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ConfinedAccordion : MonoBehaviour 
{
    public Vector2 TotalSize;
    private List<SizeProvider> SubElements;

    void Awake()
    {
        var rectTrans = transform as RectTransform;
        //Vector3[] corners = new Vector3[4];
        //rectTrans.GetLocalCorners(corners);
        //Debug.Log(corners[0]);
        //Debug.Log(corners[1]);
        //Debug.Log(corners[2]);
        //Debug.Log(corners[3]);
        //Debug.Log("");
        //Debug.Log(rectTrans.sizeDelta);
        //Debug.Log(rectTrans.rect.size);
        //Debug.Log(rectTrans.rect.width);
        //Debug.Log(rectTrans.rect.height);
        //TotalSize = rectTrans.sizeDelta;
        //TotalSize = rectTrans.rect.size;
        TotalSize = new Vector2(Screen.width, Screen.height);
        Debug.Log(TotalSize);
    }

	void Start () 
    {
        var maxListSize = TotalSize;
        SubElements = new List<SizeProvider>();
        foreach (var button in GetComponentsInChildren<SizeProvider>())
        {
            SubElements.Add(button);
            maxListSize -= button.minButtonSize;
            
        }
        foreach (var item in SubElements)
        {
            item.maxListSize = maxListSize;
            item.SetExpanded(false);
        }
        SubElements.FirstOrDefault().SetExpanded(true);
	}
	
	public void ResetAll()
    {
        foreach (var item in SubElements)
        {
            item.SetExpanded(false);
        }
    }

    public void OnClickActionUpdate()
    {
        float delta = 0.0f;
        foreach (var item in SubElements)
        {
            var rectTrans = item.transform as RectTransform;
            var pos = rectTrans.anchoredPosition;
            var size = item.minButtonSize;
            pos.y = delta - (size.y / 2);
            rectTrans.anchoredPosition = pos;
            delta -= size.y;
            delta -= item.GetListPanelCurrentSize().y;
        }
    }
}
