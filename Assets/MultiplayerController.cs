using UnityEngine;
using System.Collections;

public class MultiplayerController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		PhotonNetwork.ConnectUsingSettings("1.0");
		//PhotonNetwork.JoinOrCreateRoom("Wolrd1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
