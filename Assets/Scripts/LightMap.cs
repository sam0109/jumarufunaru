using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightMap : MonoBehaviour {
	public float lightBlockingLevel;
	public float sunBrightness;
	public float spreadCoefficient;
	public loadLevel level;
	public List<float[]> levelLighting;
	public List<LightSource> lights;
	public GameObject player;
	Rect lightMe;
	
	void Awake(){
		lights = new List<LightSource>{};
	}
	
	void Start () {
		levelLighting = new List<float[]>{};
		growLightMap();
	}

	void Update (){
		castSun();
	}

	public void growLightMap(){
		while(levelLighting.Count < level.mapPos.Count){
			levelLighting.Add(new float[loadLevel.chunkSize]);
		}
		castSun();
	}

	void castSun(){
		lightMe = player.GetComponent<RenderWindow>().renderMe;
		for(int i = Mathf.Max(0, Mathf.RoundToInt(lightMe.x)); i < level.mapPos.Count && i <= lightMe.xMax; i++){
			updateColumn(i);
		}
		for(int i = Mathf.Max(0, Mathf.RoundToInt(lightMe.x)); i < level.mapPos.Count && i <= lightMe.xMax; i++){
			for(int j = loadLevel.chunkSize - 1; j >= 0 ; j--){
				spread(i, j, levelLighting[i][j]);
			}
		}
		foreach(LightSource light in lights){
			levelLighting[Mathf.RoundToInt(light.transform.position.x)][Mathf.RoundToInt(light.transform.position.y)] = light.intensity;
			spread(Mathf.RoundToInt(light.transform.position.x), Mathf.RoundToInt(light.transform.position.y), light.intensity);
		}
	}

	void updateColumn(int col){
		float sunShaft = sunBrightness;
		for(int j = loadLevel.chunkSize - 1; j >= 0 ; j--){
			levelLighting[col][j] = sunShaft;
			if(level.mapPos[col][j] != 0x00000000){
				sunShaft -= lightBlockingLevel;
				if(sunShaft <= 0){
					sunShaft = 0;
				}
			}
		}
	}

	void updateLight(){
		castSun();
	}
	void spread(int col, int row, float originValue){
		float spreadValue = originValue - lightBlockingLevel * spreadCoefficient;
		if(col + 1 < level.mapPos.Count - 1 && spreadValue > levelLighting[col + 1][row]){
			levelLighting[col + 1][row] = spreadValue;
			spread(col + 1, row, spreadValue);
		}
		if( col - 1 >= 0 && spreadValue > levelLighting[col - 1][row]){
			levelLighting[col - 1][row] = spreadValue;
			spread(col - 1, row, spreadValue);
		}
		if(row + 1 < loadLevel.chunkSize - 1 && spreadValue > levelLighting[col][row + 1]){
			levelLighting[col][row + 1] = spreadValue;
			spread(col, row + 1, spreadValue);
		}
		if(row - 1 >= 0 && spreadValue > levelLighting[col][row - 1]){
			levelLighting[col][row - 1] = spreadValue;
			spread(col, row - 1, spreadValue);
		}
	}
}








