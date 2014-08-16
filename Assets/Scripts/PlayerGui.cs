using UnityEngine;
using System.Collections;

public class PlayerGui : MonoBehaviour {
	public Inventory inventory;
	public GUIContent TopBar;
	public GUIStyle TopBarStyle;
	public GUIStyle DirtGuiSyle;
	public GUIStyle GrassGuiSyle;
	public GUIStyle RockGuiSyle;
	public int selected;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		GUI.BeginGroup(new Rect(Screen.width/2 - 500, 0, 1000, 100));
			GUI.Box(new Rect(0,0,1000,100), TopBar, TopBarStyle);
			if(GUI.Button(new Rect(10,10,80,80), inventory.DirtNum.ToString(), DirtGuiSyle))
			{
				selected = 1;
			}
			if(GUI.Button(new Rect(110,10,80,80), inventory.GrassNum.ToString(), GrassGuiSyle))
			{
				selected = 2;
			}
			if(GUI.Button(new Rect(210,10,80,80), inventory.RockNum.ToString(), RockGuiSyle))
			{
				selected = 3;
			}
		GUI.EndGroup();
	}
}
