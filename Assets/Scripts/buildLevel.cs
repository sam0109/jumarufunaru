using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buildLevel : MonoBehaviour
{
	public GameObject player;
	public loadLevel levelData;
	public Rect renderMe;
	public GameObject air; //0x00000000
	public GameObject dirt; //0x00000001
	public GameObject grass; //0x00000002
	public GameObject stone; //0x00000003
	public List<GameObject[]> rendered;
	public LightMap lightMap;

	void Start(){
		rendered = new List<GameObject[]>();
	}

	void Update(){
		renderMe = player.GetComponent<RenderWindow>().renderMe;
		for(int i = Mathf.RoundToInt(renderMe.x); i <= renderMe.xMax; i++){
			if(i <= rendered.Count){
				rendered.Add(new GameObject[loadLevel.chunkSize]);
			}
			for(int j = Mathf.RoundToInt(renderMe.y); j <= renderMe.yMax; j++){
				if(i >= 0 && j >= 0 && j < loadLevel.chunkSize && rendered[i][j] == null){
				
					while(i >= levelData.mapPos.Count){
						levelData.mapPos.Add(generateColumn(i));
						lightMap.growLightMap();
					}
					
					switch(levelData.mapPos[i][j]){
					case(0):
						break;
					case(1):
						rendered[i][j] = (GameObject)Instantiate(dirt, new Vector2(i, j), Quaternion.identity);
						break;
					case(2):
						rendered[i][j] = (GameObject)Instantiate(grass, new Vector2(i, j), Quaternion.identity);							
						break;
					case(3):
						rendered[i][j] = (GameObject)Instantiate(stone, new Vector2(i, j), Quaternion.identity);
						break;
					default:
						print ("Unknown Block: " + levelData.mapPos[i][j]);
						break;
					}
				}
			}
		}
	}
	
	byte[] generateColumn(int xCoord){ 						//TODO: make better map generation logic (hills, mountains, valleys, etc).
		byte[] newColumn = new byte[loadLevel.chunkSize];
		for(int i = 0; i < loadLevel.chunkSize; i++){
			if(i > 895)
				newColumn[i] = 0x00000000;
			else if ( i == 895)
				newColumn[i] = 0x00000002;
			else if (i < 895)
				newColumn[i] = 0x00000001;
		}
		return newColumn;
	}
}
