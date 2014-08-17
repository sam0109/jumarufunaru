using UnityEngine;
using System.Collections;

public class GetShadow : MonoBehaviour {
	public int posX, posY;
	public LightMap lightMap;
	public float shadowLevel;
	public Sprite[] shadowSprite;
	// Use this for initialization
	void Start () {
		lightMap = GameObject.Find("Terrain").GetComponent<LightMap>();
		posX = Mathf.RoundToInt(transform.position.x);
		posY = Mathf.RoundToInt(transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<SpriteRenderer>().sprite = shadowSprite[Mathf.RoundToInt(lightMap.levelLighting[posX][posY])];
	}
}
