using UnityEngine;
using System.Collections;

public class LightSource : MonoBehaviour {
	LightMap levelLighting;
	public int intensity;
	int posX;
	int posY;
	// Use this for initialization
	void Start () {
		posX = 0;
		posY = 0;
		levelLighting = GameObject.Find("Terrain").GetComponent<LightMap>();
		levelLighting.lights.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(posX != Mathf.RoundToInt(transform.position.x) || posY != Mathf.RoundToInt(transform.position.x)){
			levelLighting.updateLight();
			posX = Mathf.RoundToInt(transform.position.x);
			posY = Mathf.RoundToInt(transform.position.y);
		}
	}
}
