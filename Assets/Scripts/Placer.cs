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
			   level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)].tag == "air" &&
			   (
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x - 1))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y))].tag != "air" ||
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x + 1))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y))].tag != "air" ||
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y - 1))].tag != "air" ||
				level.rendered[Mathf.Max(0, Mathf.RoundToInt(mousePos.x))][Mathf.Max(0, Mathf.RoundToInt(mousePos.y + 1))].tag != "air"
			   ))
			{
				
				switch(pGUI.selected)
				{
				case(1): //Dirt
					Destroy(level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)]);
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = (GameObject)Instantiate(dirt, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)].GetComponent<takeDamage>().level = levelData;
					break;
				case(2): //Grass
					Destroy(level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)]);
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = (GameObject)Instantiate(grass, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)].GetComponent<takeDamage>().level = levelData;
					break;
				case(3): //Stone
					Destroy(level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)]);
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)] = (GameObject)Instantiate(stone, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					level.rendered[Mathf.RoundToInt(mousePos.x)][Mathf.RoundToInt(mousePos.y)].GetComponent<takeDamage>().level = levelData;
					break;
				default:
					print ("Unknown Selection: " + pGUI.selected);
					break;
				}
			}
		}
	}
}
