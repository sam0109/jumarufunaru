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
	public GameObject genericBlock;
	public LightMap lightMap;

	//pooling variables

	public Dictionary<Point, GameObject> blockArray;
	IntRect blockLocations;

	void Start(){
		renderMe = player.GetComponent<RenderWindow>().renderMe;
		blockLocations = new IntRect(Mathf.RoundToInt(renderMe.x), Mathf.RoundToInt(renderMe.y), Mathf.RoundToInt(renderMe.width), Mathf.RoundToInt(renderMe.height));
		blockArray = new Dictionary<Point, GameObject>(100);

		while(blockLocations.x + blockLocations.width + 1 > levelData.mapPos.Count){
			levelData.mapPos.Add(generateColumn(blockLocations.x + blockLocations.width));
		}

		lightMap.growLightMap();

		for(int i = blockLocations.x; i <= blockLocations.x + blockLocations.width; i++){
			for(int j = Mathf.RoundToInt(blockLocations.y); j <= blockLocations.y + blockLocations.height; j++){
				blockArray.Add(new Point(i, j), (GameObject)Instantiate(genericBlock, new Vector2(i, j), Quaternion.identity));
			}
		}
		print ("Rendered");
	}

	void Update(){
		renderMe = player.GetComponent<RenderWindow>().renderMe;

		while(renderMe.x < blockLocations.x - 1){
			print ("Pushing Left");
			blockLocations.x -= 1;
			GameObject currentBlock;
			for(int i = blockLocations.y; i < blockLocations.y + blockLocations.height + 1; i++){
				currentBlock = blockArray[new Point(blockLocations.x + blockLocations.width + 1, i)];
				currentBlock.transform.position = new Vector2(blockLocations.x, i);
				currentBlock.GetComponent<AssignBlock>().SetupBlock();
				blockArray.Remove(new Point(blockLocations.x + blockLocations.width + 1, i));
				blockArray.Add(new Point(blockLocations.x, i), currentBlock);
			}
		}

		while(renderMe.x > blockLocations.x + 1){
			print ("Pushing Right");
			blockLocations.x += 1;
			if(blockLocations.x + blockLocations.width + 1 > levelData.mapPos.Count){
				levelData.mapPos.Add(generateColumn(blockLocations.x + blockLocations.width));
				lightMap.growLightMap();
			}
			GameObject currentBlock;
			for(int i = blockLocations.y; i < blockLocations.y + blockLocations.height + 1; i++){
				currentBlock = blockArray[new Point(blockLocations.x - 1, i)];
				currentBlock.transform.position = new Vector2(blockLocations.x + blockLocations.width, i);
				currentBlock.GetComponent<AssignBlock>().SetupBlock();
				blockArray.Remove(new Point(blockLocations.x - 1, i));
				blockArray.Add(new Point(blockLocations.x + blockLocations.width, i), currentBlock);
			}
		}

		while(renderMe.y < blockLocations.y - 1){
			print ("Pushing Down");
			blockLocations.y -= 1;
			GameObject currentBlock;
			for(int i = blockLocations.x; i < blockLocations.x + blockLocations.width + 1; i++){
				currentBlock = blockArray[new Point(i, blockLocations.y + blockLocations.height + 1)];
				currentBlock.transform.position = new Vector2(i, blockLocations.y);
				currentBlock.GetComponent<AssignBlock>().SetupBlock();
				blockArray.Remove(new Point(i, blockLocations.y + blockLocations.height + 1));
				blockArray.Add(new Point(i, blockLocations.y), currentBlock);
			}
		}

		while(renderMe.y > blockLocations.y + 1){
			print ("Pushing Up");
			blockLocations.y += 1;
			GameObject currentBlock;
			for(int i = blockLocations.x; i < blockLocations.x + blockLocations.width + 1; i++){
				currentBlock = blockArray[new Point(i, blockLocations.y - 1)];
				currentBlock.transform.position = new Vector2(i, blockLocations.y + blockLocations.height);
				currentBlock.GetComponent<AssignBlock>().SetupBlock();
				blockArray.Remove(new Point(i, blockLocations.y - 1));
				blockArray.Add(new Point(i, blockLocations.y + blockLocations.height), currentBlock);
			}
		}

		/*renderMe = player.GetComponent<RenderWindow>().renderMe;
		for(int i = Mathf.RoundToInt(renderMe.x); i <= renderMe.x + blockLocations.width; i++){
			if(i <= rendered.Count){
				rendered.Add(new GameObject[loadLevel.chunkSize]);
			}
			for(int j = Mathf.RoundToInt(renderMe.y); j <= renderMe.y + blockLocations.height; j++){
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
		}*/
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

public struct Point
{
	public int x, y;

	public Point(int xPos, int yPos){
		x = xPos;
		y = yPos;
	}
}

public struct IntRect
{
	public int x, y, width, height;
	
	public IntRect(int xPos, int yPos, int xWidth, int yHeight){
		x = xPos;
		y = yPos;
		width = xWidth;
		height = yHeight;
	}
}
