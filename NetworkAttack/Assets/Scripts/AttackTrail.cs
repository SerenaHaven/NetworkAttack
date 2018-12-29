using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackTrail : MonoBehaviour {
	private Transform relative;
	private Vector3 from;
	private Vector3 to;
	private Vector3 fromDirection;
	private Vector3 toDirection;
	private Vector3 centerPoint;

	private float step = 0.0f;
	private float factor = 0.0f;
	private Color color = Color.white;
	private float trailLifetime = 5.0f;
	private float timer = 0.0f;

	private TrailRenderer trailRenderer;


	void Update()
	{
		if (factor < 1.0f) {
			factor += step * Time.deltaTime;
			Vector3 current = relative.TransformDirection (Vector3.Slerp (fromDirection, toDirection, factor));
			current = current + this.centerPoint;
			this.transform.position = relative.TransformPoint (current);
			timer = Time.time;
		} else {
			if (Time.time - timer >= trailLifetime) {
				this.trailRenderer.enabled = false;
				this.transform.position = relative.TransformPoint (from);
				factor = 0.0f;
				this.trailRenderer.enabled = true;
			}
		}
	}

	public void Initialize(Transform relative, Vector3 from, Vector3 to, Color color, float duration = 4.0f, float trailLifetime = 4.0f)
	{
		this.relative = relative;
		this.from = from;
		this.to = to;
		this.factor = 0.0f;
		duration = duration == 0 ? 1.0f : duration;
		this.step = 1.0f / duration;

		this.centerPoint = Vector3.Lerp(this.from, this.to, 0.5f) * 0.9f;
		this.fromDirection = this.from - this.centerPoint;
		this.toDirection = this.to - this.centerPoint;
		this.color = color;

		this.trailRenderer = this.trailRenderer ?? this.GetComponent<TrailRenderer> ();
		this.trailRenderer.time = trailLifetime;
		this.trailLifetime = trailLifetime;
		this.trailRenderer.startColor = this.color;
	}
}