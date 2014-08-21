using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour {
	public Inventory inv;
	public PlayerGui pGUI;
	public float reachDistance;
	Vector3 mousePos;
	buildLevel level;
	loadLevel levelData;
	// Use this for initialization
	void Start () 
	{
		level = GameObject.Find("Terrain").GetComponent<buildLevel>();
		levelData = GameObject.Find("Terrain").GetComponent<loadLevel>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1)) 
		{
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 15f));
			if(mousePos.x >= transform.position.x - reachDistance && 
				mousePos.x <= transform.position.x + reachDistance && 
				mousePos.y >= transform.position.y - reachDistance && 
				mousePos.y <= transform.position.y + reachDistance && 
			  	levelData.mapPos[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] == 0x00000000 &&
			   (
				levelData.mapPos[Mathf.Max(0, Mathf.RoundToInt(mousePos.x - 1))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y))] != 0x00000000 ||
				levelData.mapPos[Mathf.Max(0, Mathf.RoundToInt(mousePos.x + 1))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y))] != 0x00000000 ||
				levelData.mapPos[Mathf.Max(0, Mathf.RoundToInt(mousePos.x))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y - 1))] != 0x00000000 ||
				levelData.mapPos[Mathf.Max(0, Mathf.RoundToInt(mousePos.x))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y + 1))] != 0x00000000
			   ))
			{
				
				switch(pGUI.selected)
				{
				case(1): //Dirt
					levelData.mapPos[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = 0x00000001;
					break;
				case(2): //Grass
					levelData.mapPos[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = 0x00000002;
					break;
				case(3): //Stone
					levelData.mapPos[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = 0x00000003;
					break;
				default:
					print ("Unknown Selection: " + pGUI.selected);
					break;
				}
				level.blockArray[new Point(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y))].GetComponent<AssignBlock>().SetupBlock();
			}
		}
	}
}
