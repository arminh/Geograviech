using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 100), "Test"))
        {
            //GameManager.instance().createCatchViechUI();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
