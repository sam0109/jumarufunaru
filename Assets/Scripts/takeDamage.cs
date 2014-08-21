using UnityEngine;
using System.Collections;

public class takeDamage : MonoBehaviour {
	public byte dataValue;

	public float maxHealth;
	public float currentHealth;

	public float healRate;
	float healTimer;

	public GameObject particles;

	public loadLevel levelData;
	public AssignBlock block;

	public Sprite[] damageStages;
	SpriteRenderer sprite;
	public int damageStage;
	int currentDamageStage;

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
		healTimer = healRate;
		currentHealth = maxHealth;
		currentDamageStage = damageStage;
	}

	// Update is called once per frame
	void Update () {
		if(currentHealth < maxHealth){
			healTimer -= Time.deltaTime;
			if(healTimer <= 0){
				currentHealth ++;
				healTimer = healRate;
			}
		}
		damageStage = Mathf.CeilToInt((1 - currentHealth / maxHealth) * 4);
		if(currentDamageStage != damageStage){
			sprite.sprite = damageStages[damageStage];
			currentDamageStage = damageStage;
		}

	}

	void damage(float amount){
		currentHealth -= amount;
		if(currentHealth <= 0){
			levelData.mapPos[Mathf.RoundToInt(transform.position.x)][Mathf.RoundToInt(transform.position.y)] = 0x00000000;
			Instantiate(particles, transform.position, Quaternion.identity);
			currentHealth = maxHealth;
			block.SetupBlock();
		}
	}
}
