using UnityEngine;
using System.Collections;

public class followPlayer : MonoBehaviour {
	public GameObject player;
	public float moveSpeed;
	public float jumpHeight;
	public float maxJumpSpeed;
	public float climbSpeed;
	public float playerDamage;
	public float groundDamage;
	float direction; //right = 1, left = -1
	bool canJump;
	bool isClimbing;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Character");
		canJump = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Turn to player
		if(player.transform.position.x > transform.position.x)
		{
			transform.localScale = new Vector3(10, 10, 10);
			direction = 1;
		} 
		else
		{
			transform.localScale = new Vector3(-10, 10, 10);
			direction = -1;
		}
		//follow player
		rigidbody2D.AddForce(transform.right * moveSpeed * direction);

		//jump if below the player
		if(player.transform.position.x > transform.position.x - .1f && player.transform.position.x < transform.position.x + .1f && player.transform.position.y > transform.position.y + .1f)
		{
			Jump();
		}

		//jump if against a wall, or climb a zombie
		RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x + .5f * direction , transform.position.y - .9f), transform.right, .01f);
		if(ray.collider != null && player.transform.position.y > transform.position.y)
		{
			if(ray.collider.tag == "zombie")
			{
				rigidbody2D.AddForce(transform.up * climbSpeed);
				ray.collider.rigidbody2D.AddForce(-transform.up * climbSpeed);
				isClimbing = true;
			}
			Jump();
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			coll.gameObject.SendMessage("playerDamage", playerDamage);
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		canJump = true;
	}

	void OnCollisionStay2D (Collision2D coll){
		if(player.transform.position.y < transform.position.y + .1f){
			coll.gameObject.SendMessage("damage", groundDamage * Time.deltaTime, SendMessageOptions.DontRequireReceiver);
		}
		else if(coll.gameObject.transform.position.y > transform.position.y - .9f){
			coll.gameObject.SendMessage("damage", groundDamage * Time.deltaTime, SendMessageOptions.DontRequireReceiver);
		}
	}



	void Jump()
	{
		if(canJump == true && rigidbody2D.velocity.y < maxJumpSpeed)
		{
			rigidbody2D.AddForce(transform.up * jumpHeight);
			RaycastHit2D rayDown = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 2f), -transform.up, .01f);
			if(rayDown.collider != null && rayDown.collider.rigidbody2D != null)
			{
				rayDown.collider.rigidbody2D.AddForce(-transform.up * jumpHeight);
			}
			canJump = false;
		}
	}
}
