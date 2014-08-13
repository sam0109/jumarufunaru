using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public GameObject bulletShotEffect;
	public float shootDelay;
	public float damage;
	public float bulletKnockback;
	float shootDelayTimer;
	// Use this for initialization
	void Start () {
		shootDelayTimer = shootDelay;
	}
	
	// Update is called once per frame
	void Update () {
		shootDelayTimer -= Time.deltaTime;
		if(Input.GetMouseButtonDown(0) && shootDelayTimer <= 0){
			shootDelayTimer = shootDelay;
			transform.GetComponentInChildren<ParticleSystem>().Play();
			RaycastHit2D bullet = Physics2D.Raycast(transform.position, transform.up);
			if(bullet.collider != null && bullet.collider.tag == "zombie"){
				bullet.collider.gameObject.SendMessage("Shot", new BulletInfo(transform.position, damage, bulletKnockback));
			}
		}
	}
}

struct BulletInfo {
	public Vector3 bulletOrigin;
	public float bulletDamage;
	public float bulletKnockback;
	public BulletInfo(Vector3 origin, float damage, float knockback){
		bulletOrigin = origin;
		bulletDamage = damage;
		bulletKnockback= knockback;
	}
}
