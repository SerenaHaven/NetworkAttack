using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {
	private GameObject gameObjectEarth = null;

	private AttackData data;

	void Start () {
		gameObjectEarth = GameObject.Find ("Earth");
		TextAsset textAsset = Resources.Load<TextAsset> ("Data");
		Debug.Log (textAsset.text);
		data = JsonUtility.FromJson<AttackData> (textAsset.text);
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

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		for (int i = -180; i <= 180; i += 30) {
			for (int j = -90; j <= 90; j += 30) {
				Gizmos.DrawSphere (LLToVector3 (1, i, j), 0.01f);
			}
		}
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