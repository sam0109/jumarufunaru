using UnityEngine;
using System.Collections;

public class MainMenuGui : MonoBehaviour {
	bool hostServerMenu;
	bool joinServerMenu;
	bool inLobby;
	public string worldTitle = "";
	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("v0.0.1");
		hostServerMenu = false;
		inLobby = false;
	}

	void OnGUI(){
		GUI.BeginGroup(new Rect(Screen.width/4, 50, Screen.width/2, 500));
		if(!hostServerMenu && !joinServerMenu){
			GUI.Box(new Rect(0, 0, Screen.width/2, 500), "");
			if(GUI.Button(new Rect(50, 50, Screen.width/2 - 100, 100), "Start")){
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(50, 200, Screen.width/2 - 100, 100), "Join Room")){
				joinServerMenu = true;
			}
			if(GUI.Button(new Rect(50, 350, Screen.width/2 - 100, 100), "Create Room")){
				hostServerMenu = true;
			}
		}
		else if(hostServerMenu){
			GUI.Box(new Rect(0, 0, Screen.width/2, 500), "");
			GUI.Label(new Rect(50, 50, Screen.width/2 - 100, 100), "Room Name:");
			worldTitle = GUI.TextField(new Rect(50, 75, Screen.width/2 - 100, 25), worldTitle, 30);
			if(GUI.Button(new Rect(50, 125, Screen.width/2 - 100, 100), "Host") && worldTitle != ""){
				if(inLobby == true){
					PhotonNetwork.CreateRoom(worldTitle);
					Debug.Log("Created room");
					Application.LoadLevel(1);
				}
				else{
					Debug.Log("Not in lobby");
				}
			}
			if(GUI.Button(new Rect(50, 350, Screen.width/2 - 100, 100), "Back")){
				hostServerMenu = false;
			}
		}
		else if(joinServerMenu){
			GUI.Box(new Rect(0, 0, Screen.width/2, 500), "");
			GUI.Label(new Rect(50, 50, Screen.width/2 - 100, 100), "Room Name:");
			worldTitle = GUI.TextField(new Rect(50, 75, Screen.width/2 - 100, 25), worldTitle, 30);
			if(GUI.Button(new Rect(50, 125, Screen.width/2 - 100, 100), "Join") && worldTitle != ""){
				if(inLobby == true){
					PhotonNetwork.JoinRoom(worldTitle);
					Debug.Log("Joined room");
					Application.LoadLevel(1);
				}
				else{
					Debug.Log("Not in lobby");
				}
			}
			if(GUI.Button(new Rect(50, 350, Screen.width/2 - 100, 100), "Back")){
				joinServerMenu = false;
			}
		}
		GUI.EndGroup();
	}
	void OnJoinedLobby()
	{
		inLobby = true;
	}
}
