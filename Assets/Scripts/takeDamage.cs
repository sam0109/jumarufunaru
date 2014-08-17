using UnityEngine;
using System.Collections;

public class takeDamage : MonoBehaviour {
	public byte dataValue;
	public float maxHealth;
	public float currentHealth;
	public float healRate;
	float healTimer;
	public GameObject particles;
	loadLevel level;
	LightMap shadow;

	// Use this for initialization
	void Start () {
		level = GameObject.Find("Terrain").GetComponent<loadLevel>();
		shadow = GameObject.Find("Terrain").GetComponent<LightMap>();
		level.mapPos[Mathf.RoundToInt(transform.position.x)][Mathf.RoundToInt(transform.position.y)] = dataValue;
		healTimer = healRate;
		currentHealth = maxHealth;
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
		transform.GetComponentInChildren<damageOverlay>().damageStage = Mathf.CeilToInt((1 - currentHealth / maxHealth) * 4);

	}

	void damage(float amount){
		currentHealth -= amount;
		if(currentHealth <= 0){
			shadow.updateLight();
			level.mapPos[Mathf.RoundToInt(transform.position.x)][Mathf.RoundToInt(transform.position.y)] = 0x00000000;
			Instantiate(particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
