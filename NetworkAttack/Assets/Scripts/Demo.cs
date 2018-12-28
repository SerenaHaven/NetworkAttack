using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {
	private GameObject gameObjectEarth = null;

	private AttackData data;
	private GameObject trailTemplate;
	private GameObject markTemplate;

	private float earthRadius = 0.4f;
	private float duration = 3.0f;
	private float trailLifetime = 2.0f;

	private GameObjectPool trailPool;
	private GameObjectPool markPool;

	void Start () {
		trailTemplate = Resources.Load<GameObject> ("Trail");
		markTemplate = Resources.Load<GameObject> ("Mark");

		trailPool = trailPool ?? new GameObjectPool (trailTemplate);
		markPool = markPool ?? new GameObjectPool (markTemplate);

		gameObjectEarth = GameObject.Find ("Earth");
		TextAsset textAsset = Resources.Load<TextAsset> ("Data");
		data = JsonUtility.FromJson<AttackData> (textAsset.text);

		for (int i = 0; i < data.infos.Length; i++) {
			Vector3 from = LLToVector3 (earthRadius, data.infos [i].location [0], data.infos [i].location [1]);
			Vector3 to = LLToVector3 (earthRadius, data.infos [i].location [2], data.infos [i].location [3]);
			Color color = Random.ColorHSV (0.25f, 0.55f, 0.5f, 1.0f, 1.0f, 1.0f); 

			GameObject gameObjectMark = markPool.Spawn ();
			gameObjectMark.GetComponent<Mark> ().Initialize (color);
			gameObjectMark.transform.SetParent (gameObjectEarth.transform);
			gameObjectMark.transform.localPosition = from;
			gameObjectMark = markPool.Spawn ();
			gameObjectMark.GetComponent<Mark> ().Initialize (color);
			gameObjectMark.transform.SetParent (gameObjectEarth.transform);
			gameObjectMark.transform.localPosition = to;

			GameObject gameObjectTrail = GameObject.Instantiate (trailTemplate, from, Quaternion.identity);
			AttackTrail trail = gameObjectTrail.AddComponent<AttackTrail> ();
			trail.Initialize (this.transform, from, to, color, Random.Range (0.5f, 1.0f) * duration, Random.Range (0.5f, 1.0f) * trailLifetime);
		}
	}
		
	/// <summary>
	/// 经纬度转坐标
	/// </summary>
	/// <returns>The to vector3.</returns>
	/// <param name="radius">半径.</param>
	/// <param name="longitude">经度.-180（东） ~ 180（西）</param>
	/// <param name="latitude">纬度.-90 ~ 90</param>
	public Vector3 LLToVector3(float radius, float longitude, float latitude)
	{
		float radLatitude = Mathf.Deg2Rad * latitude;
		float radLongitude = Mathf.Deg2Rad * longitude;
		return new Vector3 (-Mathf.Cos (radLatitude) * Mathf.Sin (radLongitude), Mathf.Sin (radLatitude), Mathf.Cos (radLatitude) * Mathf.Cos (radLongitude)) * radius;
	}
}

[System.Serializable]
public class AttackData{
	public AttackInfo[] infos;
}

[System.Serializable]
public class AttackInfo{
	public string from;
	public string to;
	public float[] location;
}