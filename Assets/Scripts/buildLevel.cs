using UnityEngine;
using System.Collections;
using System.Linq;

public class buildLevel : MonoBehaviour
{
	public GameObject player;
	public loadLevel levelData;
	public Rect renderMe;
	public GameObject air; //0x00000000
	public GameObject dirt; //0x00000001
	public GameObject grass; //0x00000002
	public GameObject stone; //0x00000003
	public GameObject[,] rendered;
	public int[] levelSize;

	void Start(){
		levelSize = new int[]{levelData.mapPos.Count, levelData.mapPos[0].Length};
		rendered = new GameObject[levelSize[0], levelSize[1]];
		renderMe = player.GetComponent<RenderWindow>().renderMe;
	}

	void Update(){
		renderMe = player.GetComponent<RenderWindow>().renderMe;
		for(int i = Mathf.RoundToInt(renderMe.x); i <= renderMe.xMax; i++){
			for(int j = Mathf.RoundToInt(renderMe.y); j <= renderMe.yMax; j++){
				if(i >= 0 && j >= 0 && j < levelSize[1] && i < levelSize[0] && rendered[i, j] == null){
					switch(levelData.mapPos[i][j]){
					case(0):
						//rendered[i,j] = (GameObject)Instantiate(air, new Vector2(i, j), Quaternion.identity);
						break;
					case(1):
						rendered[i,j] = (GameObject)Instantiate(dirt, new Vector2(i, j), Quaternion.identity);
						break;
					case(2):
						rendered[i,j] = (GameObject)Instantiate(grass, new Vector2(i, j), Quaternion.identity);							
						break;
					case(3):
						rendered[i,j] = (GameObject)Instantiate(stone, new Vector2(i, j), Quaternion.identity);
						break;
					default:
						print ("Unknown Block: " + levelData.mapPos[i][j]);
						break;
					}
				}
			}
		}
	} 
}
