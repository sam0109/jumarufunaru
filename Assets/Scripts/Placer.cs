using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour {
	public GameObject dirt;
	public GameObject grass;
	public GameObject stone;
	public Inventory inv;
	public PlayerGui pGUI;
	public float reachDistance;
	LightMap shadow;
	Vector3 mousePos;
	buildLevel level;
	// Use this for initialization
	void Start () 
	{
		shadow = GameObject.Find("Terrain").GetComponent<LightMap>();
		level = GameObject.Find("Terrain").GetComponent<buildLevel>();
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
			   level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] == null &&
			   (
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x - 1))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y))] != null ||
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x + 1))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y))] != null ||
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y - 1))] != null ||
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y + 1))] != null
			   ))
			{
				
				switch(pGUI.selected)
				{
				case(1): //Dirt
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = (GameObject)Instantiate(dirt, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					break;
				case(2): //Grass
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = (GameObject)Instantiate(grass, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					break;
				case(3): //Stone
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = (GameObject)Instantiate(stone, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					break;
				default:
					print ("Unknown Selection: " + pGUI.selected);
					break;
				}
				shadow.updateLight();
			}
		}
	}
}
