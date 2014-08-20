using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buildLevel : MonoBehaviour
{
	public float mapSeed;
	public float terrainDetailWidth;
	public float terrainHeightMultiplyer;
	public GameObject player;
	public loadLevel levelData;
	public Rect renderMe;
	public GameObject shadow;
	GameObject shadowSetter;
	public GameObject air; //0x00000000
	public GameObject dirt; //0x00000001
	public GameObject grass; //0x00000002
	public GameObject stone; //0x00000003
	public GameObject genericBlock;
	public List<GameObject[]> rendered;
	public LightMap lightMap;

	//pooling variables

	public List<GameObject> blocks;

	void Start(){
		rendered = new List<GameObject[]>();
		/*renderMe = player.GetComponent<RenderWindow>().renderMe;

		for(int i = Mathf.RoundToInt(renderMe.x); i <= renderMe.xMax; i++){
			for(int j = Mathf.RoundToInt(renderMe.y); j <= renderMe.yMax; j++){
				blocks.Add((GameObject)Instantiate(genericBlock, new Vector2(i, j), Quaternion.identity));
			}
		} */
	}

	void Update(){
		/*renderMe = player.GetComponent<RenderWindow>().renderMe;
		foreach(GameObject block in blocks){
			if(!renderMe.Contains(block.transform.position)){
				block.SetActive(false);
			}
		}
		bool foundBlock;
		for(int i = Mathf.RoundToInt(renderMe.x); i <= renderMe.xMax; i++){
			for(int j = Mathf.RoundToInt(renderMe.y); j <= renderMe.yMax; j++){
				foundBlock = false;
				foreach(GameObject block in blocks){
					if(block.GetActive() == true && Mathf.RoundToInt(transform.position.x) == i && Mathf.RoundToInt(transform.position.y) == j){
						foundBlock = true;
					}
				}
				if(!foundBlock){
					foreach(GameObject block in blocks){
						if(block.GetActive() == false){
							block.transform.position = new Vector2(i, j);
							block.SetActive(true);
							break;
						}
					}
					Debug.Log("Ran out of blocks!");
				}
			}
		} */

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
						rendered[i][j] = (GameObject)Instantiate(air, new Vector2(i, j), Quaternion.identity);	
						shadowSetter = (GameObject)Instantiate(shadow, new Vector3(i, j, -1), Quaternion.identity);
						shadowSetter.GetComponent<GetShadow>().lightMap = lightMap;
						break;
					case(1):
						shadowSetter = (GameObject)Instantiate(shadow, new Vector3(i, j, -1), Quaternion.identity);
						shadowSetter.GetComponent<GetShadow>().lightMap = lightMap;
						rendered[i][j] = (GameObject)Instantiate(dirt, new Vector2(i, j), Quaternion.identity);
						rendered[i][j].GetComponent<takeDamage>().level = levelData;
						break;
					case(2):
						shadowSetter = (GameObject)Instantiate(shadow, new Vector3(i, j, -1), Quaternion.identity);
						shadowSetter.GetComponent<GetShadow>().lightMap = lightMap;
						rendered[i][j] = (GameObject)Instantiate(grass, new Vector2(i, j), Quaternion.identity);	
						rendered[i][j].GetComponent<takeDamage>().level = levelData;
						break;
					case(3):
						shadowSetter = (GameObject)Instantiate(shadow, new Vector3(i, j, -1), Quaternion.identity);
						shadowSetter.GetComponent<GetShadow>().lightMap = lightMap;
						rendered[i][j] = (GameObject)Instantiate(stone, new Vector2(i, j), Quaternion.identity);
						rendered[i][j].GetComponent<takeDamage>().level = levelData;
						break;
					default:
						print ("Unknown Block: " + levelData.mapPos[i][j]);
						break;
					}
				}
			}
		}
	}
	
	byte[] generateColumn(int xCoord){ 						//TODO: make better map generation logic.
		byte[] newColumn = new byte[loadLevel.chunkSize];
		int columnStart = (int) ((Mathf.PerlinNoise(xCoord * .01f / terrainDetailWidth, mapSeed) * terrainHeightMultiplyer) + 900);
		for(int i = 0; i < loadLevel.chunkSize; i++){
			if(i > columnStart)
				newColumn[i] = 0x00000000;
			else if ( i == columnStart)
				newColumn[i] = 0x00000002;
			else if (i < columnStart)
				newColumn[i] = 0x00000001;
		}
		return newColumn;
	}
}
