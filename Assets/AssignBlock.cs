using UnityEngine;
using System.Collections;

public class AssignBlock : MonoBehaviour {
	public loadLevel levelData;
	public Sprite[] textures; //0: air; 1: dirt; 2: grass; 3: stone;

	void Start () {
		levelData = GameObject.FindGameObjectWithTag("terrain").GetComponent<loadLevel>();
		SetupBlock();
	}

	void OnEnable(){
		SetupBlock();
	}

	void SetupBlock(){
		switch(levelData.mapPos[Mathf.RoundToInt(transform.position.x)][Mathf.RoundToInt(transform.position.y)]){
		case(0x00000000): //air
			gameObject.GetComponent<SpriteRenderer>().sprite = textures[0];
			break;
		case(0x00000001): //dirt
			gameObject.GetComponent<SpriteRenderer>().sprite = textures[1];
			break;
		case(0x00000002): //grass
			gameObject.GetComponent<SpriteRenderer>().sprite = textures[2];
			break;
		case(0x00000003): //stone
			gameObject.GetComponent<SpriteRenderer>().sprite = textures[3];
			break;
		}
	}
}
