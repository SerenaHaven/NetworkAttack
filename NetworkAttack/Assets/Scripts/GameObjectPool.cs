using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool {
	private GameObject template;
	private List<GameObject> activeList;
	private List<GameObject> inactiveList;

	public GameObjectPool(GameObject template)
	{
		if (template == null) {
			Debug.LogError("Template cannot be an empty value.");
		}
		this.template = template;
		activeList = new List<GameObject> ();
		inactiveList = new List<GameObject> ();
	}

	public GameObject Spawn()
	{
		GameObject result;
		if (inactiveList.Count > 0) {
			result = inactiveList [0];
			inactiveList.RemoveAt (0);
		} else {
			result = GameObject.Instantiate (template);
		}
		if (activeList.Contains (result) == false) {
			activeList.Add (result);
		}
		result.SetActive (true);
		return result;
	}

	public void Despawn(GameObject target)
	{
		if (target != null && activeList.Contains (target) == true) {
			activeList.Remove (target);
			if (inactiveList.Contains (target) == false) {
				inactiveList.Add (target);
			}
			target.SetActive (false);
		}
	}
}