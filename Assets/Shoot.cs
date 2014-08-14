using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public GameObject bulletHitEffect;
	public float shootDelay;
	float shootDelayTimer;
	public float damage;
	public float bulletKnockback;
	public float lineWidth;
	public float lineShrinkRate;
	float currentLineWidth;
	// Use this for initialization
	void Start () {
		shootDelayTimer = shootDelay;
	}
	
	// Update is called once per frame
	void Update () {
		shootDelayTimer -= Time.deltaTime;
		if(currentLineWidth > 0){
			GetComponent<LineRenderer>().SetWidth(currentLineWidth, currentLineWidth);
			currentLineWidth -= lineShrinkRate * Time.deltaTime;
		}
		else{
			GetComponent<LineRenderer>().enabled = false;
		}
		if(Input.GetMouseButtonDown(0) && shootDelayTimer <= 0){
			shootDelayTimer = shootDelay;
			transform.GetComponentInChildren<ParticleSystem>().Play();
			RaycastHit2D bullet = Physics2D.Raycast(transform.GetChild(0).position, transform.up);
			GetComponent<LineRenderer>().enabled = true;
			GetComponent<LineRenderer>().SetPosition(0, transform.GetChild(0).position);
			currentLineWidth = lineWidth;
			if(bullet.collider != null){
				GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).position + transform.GetChild(0).up * (bullet.point - (Vector2)transform.GetChild(0).position).magnitude);
				Instantiate(bulletHitEffect, bullet.point, Quaternion.identity);
				if(bullet.collider.tag == "zombie"){
					bullet.collider.gameObject.SendMessage("Shot", new BulletInfo(transform.position, damage, bulletKnockback));
				}
				else{
					bullet.collider.gameObject.SendMessage ("damage", damage, SendMessageOptions.DontRequireReceiver);
				}
			}
			else{
				GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).position + transform.GetChild(0).up * 100);
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


