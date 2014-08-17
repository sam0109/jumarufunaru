using UnityEngine;
using System.Collections;

public class characterMover : MonoBehaviour {
	public float accelerationRate;
	public float jumpHeight;
	bool isOnGround;
	// Use this for initialization
	void Start () {
		isOnGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.AddForce(Input.GetAxis("Horizontal") * Vector2.right * accelerationRate * Time.deltaTime);
		if((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && isOnGround == true){
			rigidbody2D.AddForce(Vector2.up * jumpHeight);
			isOnGround = false;
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
			isOnGround = true;
	}
}
