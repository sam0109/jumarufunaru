/*using UnityEngine;
using System.Collections;

public class Miner : MonoBehaviour
{
	Vector3 mousePos;
	public float reachDistance;
	public int damageArea;
	public float pickDamage;
	public float swingSpeed;
	float swingTimer;
	public loadLevel levelData;
	public buildLevel level;

	void Start ()
	{
	swingTimer = swingSpeed;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0) && swingTimer <= 0) {
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 15f));
			if(mousePos.x >= transform.position.x - reachDistance && 
			   mousePos.x <= transform.position.x + reachDistance && 
			   mousePos.y >= transform.position.y - reachDistance && 
			   mousePos.y <= transform.position.y + reachDistance)
			{
				for(int i = 0; i < damageArea; i++){
					for(int j = 0; j < damageArea; j++){
						level.rendered[i - damageArea/2 + Mathf.RoundToInt(mousePos.x)][j - damageArea/2 + Mathf.RoundToInt(mousePos.y)].SendMessage("damage", pickDamage, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
		swingTimer -= Time.deltaTime;
	}
} */
