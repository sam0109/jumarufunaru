using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
	public float maxHealth;
	public float currentHealth;
	public float immunityTimerStart;
	float immunityTimerCurrent;
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		immunityTimerCurrent = immunityTimerStart;
	}
	
	// Update is called once per frame
	void Update () {
		immunityTimerCurrent -= Time.deltaTime;
		if(currentHealth <= 0){
			print ("died 4 realz!");
		}
	}
	void playerDamage(float damageAmount){
		if(immunityTimerCurrent <= 0){
			currentHealth -= damageAmount;
			immunityTimerCurrent = immunityTimerStart;
		}
	}
}
