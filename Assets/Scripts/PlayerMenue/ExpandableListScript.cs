using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ExpandableListScript : MonoBehaviour
{
    public bool IsExpanded;
    private float lenght;

    public void OnListItemClick()
    {
        IsExpanded = !IsExpanded;
    }
}
