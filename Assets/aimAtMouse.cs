using UnityEngine;
using System.Collections;

public class aimAtMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {        
		transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward );
		transform.eulerAngles = new Vector3(0, 0,transform.eulerAngles.z);
	}
}
