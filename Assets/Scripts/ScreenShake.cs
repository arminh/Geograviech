using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	public Camera camera; // set this via inspector
	public float shake;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	void Update() {
		if (shake > 0.0f) {
            camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
			
		} else {
			shake = 0.0f;
		}
	}
}
