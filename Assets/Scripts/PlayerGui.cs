using UnityEngine;
using System.Collections;

public class PlayerGui : MonoBehaviour {
	public Inventory inventory;
	public GUIStyle DirtGuiSyle;
	public GUIStyle GrassGuiSyle;
	public GUIStyle RockGuiSyle;
	public int selected;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		if( GUI.Button(new Rect(0, Screen.height - Screen.width/10, Screen.width/10, Screen.width/10), inventory.DirtNum.ToString(), DirtGuiSyle))
		{
			selected = 1;
		}
		if(GUI.Button(new Rect(Screen.width/10, Screen.height - Screen.width/10, Screen.width/10, Screen.width/10),  inventory.GrassNum.ToString(), GrassGuiSyle))
		{
			selected = 2;
		}
		if(GUI.Button(new Rect(Screen.width/5, Screen.height - Screen.width/10, Screen.width/10, Screen.width/10),  inventory.RockNum.ToString(), RockGuiSyle))
		{
			selected = 3;
		}
	}
}
