using UnityEngine;
using System.Collections;

public class AssignBlock : MonoBehaviour {
	loadLevel levelData;
	LightMap lightMap;
	GetShadow myShadow;
	takeDamage damage;
	SpriteRenderer damageOverlay;

	MeshRenderer meshRenderer;
	BoxCollider2D boxCollider;
	public Material[] mats; //0: air; 1: dirt; 2: grass; 3: stone;

	void Start () {
		levelData = GameObject.FindGameObjectWithTag("terrain").GetComponent<loadLevel>();
		lightMap = GameObject.FindGameObjectWithTag("terrain").GetComponent<LightMap>();
		meshRenderer = gameObject.GetComponent<MeshRenderer>();
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
		damage = gameObject.GetComponent<takeDamage>();
		damage.levelData = levelData;
		damage.block = this;
		damageOverlay = transform.GetChild(0).GetComponent<SpriteRenderer>();;
		myShadow = gameObject.GetComponentInChildren<GetShadow>();
		myShadow.lightMap = lightMap;
		SetupBlock();
	}

	public void SetupBlock(){
		int posX = Mathf.RoundToInt(transform.position.x);
		int posY = Mathf.RoundToInt(transform.position.y);
		myShadow.posX = posX;
		myShadow.posY = posY;
		switch(levelData.mapPos[posX][posY]){
		case(0x00000000): //air
			meshRenderer.enabled = false;
			boxCollider.enabled = false;
			damage.enabled = false;
			damageOverlay.enabled = false;
			break;
		case(0x00000001): //dirt
			meshRenderer.enabled = true;
			meshRenderer.material = mats[1];
			boxCollider.enabled = true;
			damage.enabled = true;
			damage.dataValue = 0x00000001;
			damage.maxHealth = 4;
			damage.healRate = 1;
			damage.currentHealth = damage.maxHealth;
			damageOverlay.enabled = true;
			break;
		case(0x00000002): //grass
			meshRenderer.enabled = true;
			meshRenderer.material = mats[2];
			boxCollider.enabled = true;
			damage.enabled = true;
			damage.dataValue = 0x00000002;
			damage.maxHealth = 4;
			damage.healRate = 1;
			damage.currentHealth = damage.maxHealth;
			damageOverlay.enabled = true;
			break;
		case(0x00000003): //stone
			meshRenderer.enabled = true;
			meshRenderer.material = mats[3];
			boxCollider.enabled = true;
			damage.enabled = true;
			damage.dataValue = 0x00000003;
			damage.maxHealth = 8;
			damage.healRate = 0.5f;
			damage.currentHealth = damage.maxHealth;
			damageOverlay.enabled = true;
			break;
		default:
			Debug.Log("unknown texture");
			break;
		}
	}
}
