using UnityEngine;
using System.Collections;

public class zombieHealth : MonoBehaviour {
	public float maxHealth;
	public float currentHealth;
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0){
			Destroy(gameObject);
		}
	}
}
