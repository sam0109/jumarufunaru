using UnityEngine;
using System.Collections;

public class getShot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Shot(BulletInfo bullet){
		GetComponent<zombieHealth>().currentHealth -= bullet.bulletDamage;
		rigidbody2D.AddForce((transform.position - bullet.bulletOrigin).normalized * bullet.bulletKnockback);
	}
}
