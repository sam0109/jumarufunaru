using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour {
	public GameObject dirt;
	public GameObject grass;
	public GameObject stone;
	public Inventory inv;
	public PlayerGui pGUI;
	public float reachDistance;
	Vector3 mousePos;
	// Use this for initialization
	void Start () 
	{
	
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
				Physics2D.Raycast(new Vector3 (Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y), -1), Vector3.forward).collider == null &&
			   (
				Physics2D.Raycast(new Vector3 (Mathf.RoundToInt(mousePos.x - 1), Mathf.RoundToInt(mousePos.y), -1), Vector3.forward).collider != null ||
				Physics2D.Raycast(new Vector3 (Mathf.RoundToInt(mousePos.x + 1), Mathf.RoundToInt(mousePos.y), -1), Vector3.forward).collider != null ||
			    Physics2D.Raycast(new Vector3 (Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y - 1), -1), Vector3.forward).collider != null ||
				Physics2D.Raycast(new Vector3 (Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y + 1), -1), Vector3.forward).collider != null)
			   )
			{
				switch(pGUI.selected)
				{
				case(1): //Dirt
					Instantiate(dirt, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					break;
				case(2): //Grass
					Instantiate(grass, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					break;
				case(3): //Stone
					Instantiate(stone, new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), Quaternion.identity);
					break;
				default:
					print ("Unknown Selection: " + pGUI.selected);
					break;
				}
			}
		}
	}
}
