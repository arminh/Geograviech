using UnityEngine;
using System.Collections;

public class ImpMarkerScript : MonoBehaviour {


	private double[]	coordinatesWGS84 = new double[2];
	public double[]		CoordinatesWGS84;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		Application.LoadLevel ("FightScreen-Imp");
	}

}
