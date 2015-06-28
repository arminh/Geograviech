using UnityEngine;
using System.Collections;

public class OnClickReaction : MonoBehaviour 
{
	public string MonsterType;

	void OnMouseDown () 
	{
		Application.LoadLevel (MonsterType);
	}
}
