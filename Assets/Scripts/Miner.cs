using UnityEngine;
using System.Collections;

public class Miner : MonoBehaviour
{
	Vector3 mousePos;
	public float reachDistance;
	public float damageArea;
	public float pickDamage;
	public float swingSpeed;
	float swingTimer;

	void Start ()
	{
	swingTimer = swingSpeed;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0) && swingTimer <= 0) {
			ArrayList blockHits = new ArrayList (); 
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 15f));
			if(mousePos.x >= transform.position.x - reachDistance && mousePos.x <= transform.position.x + reachDistance && mousePos.y >= transform.position.y - reachDistance && mousePos.y <= transform.position.y + reachDistance){
				for (int i = 0; i < damageArea; i++) {
					for (int j = 0; j < damageArea; j++) {
						blockHits.Add (Physics2D.Raycast (new Vector3 (mousePos.x + i - damageArea / 2, mousePos.y + j - damageArea / 2, -1), Vector3.forward));
					}
				}
				ArrayList blocks = new ArrayList ();
				foreach (RaycastHit2D obj in blockHits) {
					if (!blocks.Contains (obj)) {
						if(obj.collider != null){
							blocks.Add (obj.collider.gameObject);
						}
					}
				}
				foreach (GameObject block in blocks) {
					block.SendMessage ("damage", pickDamage, SendMessageOptions.DontRequireReceiver);
				}
				swingTimer = swingSpeed;
			}
		}
		swingTimer -= Time.deltaTime;
	}
}
