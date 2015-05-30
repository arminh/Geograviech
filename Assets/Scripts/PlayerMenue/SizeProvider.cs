using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SizeProvider : MonoBehaviour 
{
    private ConfinedAccordion parent;
    public Vector2 minButtonSize { get; set; }
    public Vector2 maxListSize { get; set; }

    public RectTransform Image;
    public RectTransform ListPanel;

    public Vector2 animationStep;
    public float rotationStep;
    public bool IsExpanded;

    void Awake()
    {
        var rectTrans = transform as RectTransform;
        minButtonSize = rectTrans.sizeDelta;
    }

	void Start () 
    {
        parent = GetComponentInParent<ConfinedAccordion>();
        //foreach (RectTransform item in transform)
        //{
        //    var image = item.GetComponent<Image>();
        //    if (image != null)
        //        Image = item;
        //    var list = item.GetComponent<ScrollRect>();
        //    if (list != null)
        //        ListPanel = item;
        //}
	}

    public void Expand()
    {
        if (!IsExpanded)
        {
            parent.ResetAll();
            SetExpanded(true);
        }
    }

    public void SetExpanded(bool what)
    {
        IsExpanded = what;
        ListPanel.gameObject.SetActive(what);
    }

    public Vector2 GetListPanelCurrentSize()
    {
        return ListPanel.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsExpanded && ListPanel.sizeDelta.SqrMagnitude() != 0.0)
        {
            var step = animationStep * Time.deltaTime;
            var newSize = ListPanel.sizeDelta - step;
            newSize.x = Mathf.Clamp(newSize.x, 0.0f, maxListSize.x);
            newSize.y = Mathf.Clamp(newSize.y, 0.0f, maxListSize.y);
            ListPanel.sizeDelta = newSize;

            if (Image.rotation.z > 0.0f)
            {
                var rot = rotationStep * Time.deltaTime;
                Image.Rotate(0.0f, 0.0f, -rot, Space.Self);
            }
        }
        if (IsExpanded && ListPanel.sizeDelta.SqrMagnitude() != maxListSize.SqrMagnitude())
        {
            parent.OnClickActionUpdate();
            var step = animationStep * Time.deltaTime;
            var newSize = ListPanel.sizeDelta + step;
            newSize.x = Mathf.Clamp(newSize.x, 0.0f, maxListSize.x);
            newSize.y = Mathf.Clamp(newSize.y, 0.0f, maxListSize.y);
            ListPanel.sizeDelta = newSize;

            if (Image.rotation.z < 90.0f)
            {
                var rot = rotationStep * Time.deltaTime;
                Image.Rotate(0.0f, 0.0f, rot, Space.Self);
            }
        }
    }
}
