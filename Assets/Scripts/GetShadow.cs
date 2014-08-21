using UnityEngine;
using System.Collections;

public class GetShadow : MonoBehaviour {
	public int posX, posY;
	public LightMap lightMap;
	public float shadowLevel;
	public float shadowDeltaRate;
	float newShadowLevel;
	SpriteRenderer displayedSprite;
	// Use this for initialization
	void Start () {
		displayedSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		newShadowLevel = 1-(lightMap.levelLighting[posX][posY] / 10);
		shadowLevel = Mathf.Lerp(shadowLevel, newShadowLevel, shadowDeltaRate);
		displayedSprite.color = new Color(1f,1f,1f, shadowLevel);
	}
}
