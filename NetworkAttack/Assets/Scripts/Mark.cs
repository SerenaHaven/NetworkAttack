using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour {
	private Light halo;
	private float intensityLerpSpeed = 1.0f;
	private float maxIntensity = 3.0f;
	private float intensity = 0.0f;

	public void Initialize (Color color) {
		halo = halo ?? this.GetComponent<Light> ();
		halo.color = color;
	}
	
	void Update () {
		intensity += Time.deltaTime * intensityLerpSpeed;
		halo.intensity = Mathf.PingPong (intensity, maxIntensity);
	}
}