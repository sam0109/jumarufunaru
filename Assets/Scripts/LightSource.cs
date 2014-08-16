using UnityEngine;
using System.Collections;

public class LightSource : MonoBehaviour {
	buildLevel level;
	public int intensity;
	bool waited;
	// Use this for initialization
	void Start () {
		waited = false;
		level = GameObject.Find("Terrain").GetComponent<buildLevel>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!waited){
			waited = true;
		}
		else{
			level.rendered[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)].GetComponent<Shadow>().lightLevel = intensity;
		}
	}
}
