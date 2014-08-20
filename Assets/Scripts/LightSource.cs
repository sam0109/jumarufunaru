using UnityEngine;
using System.Collections;

public class LightSource : MonoBehaviour {
	LightMap levelLighting;
	public int intensity;
	void Start () {
		levelLighting = GameObject.Find("Terrain").GetComponent<LightMap>();
		levelLighting.lights.Add(this);
	}
}
