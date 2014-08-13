using UnityEngine;
using System.Collections;

public class RenderWindow : MonoBehaviour {
	public Rect renderMe;
	public Vector2 renderBuffer;
	// Use this for initialization
	void Awake () {
		renderMe = new Rect(transform.position.x - transform.position.x % 1  - renderBuffer.x / 2, transform.position.y - transform.position.y % 1  - renderBuffer.y / 2, renderBuffer.x, renderBuffer.y);
	}

	// Update is called once per frame
	void Update () {
		renderMe.x = transform.position.x - transform.position.x % 1  - renderBuffer.x / 2;
		renderMe.y = transform.position.y - transform.position.y % 1  - renderBuffer.y / 2;
	}
}
