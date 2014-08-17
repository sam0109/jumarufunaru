﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightMap : MonoBehaviour {
	public float lightBlockingLevel;
	public float sunBrightness;
	public float spreadCoefficient;
	public loadLevel level;
	public List<float[]> levelLighting = new List<float[]>{};
	// Use this for initialization
	void Start () {
		generateLightMap();
		castSun();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void generateLightMap(){
		for(int i = 0; i < level.mapPos.Count; i++){
			levelLighting.Add(new float[loadLevel.chunkSize]);
		}
	}
	
	public void growLightMap(){
		while(levelLighting.Count < level.mapPos.Count){
			levelLighting.Add(new float[loadLevel.chunkSize]);
		}
		updateLight();
	}
	
	void castSun(){
		for(int i = 0; i < level.mapPos.Count; i++){
			updateColumn(i);
		}
		for(int i = 0; i < level.mapPos.Count; i++){
			for(int j = loadLevel.chunkSize - 1; j >= 0 ; j--){
				spread(i, j, levelLighting[i][j]);
			}
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
	
	public void updateLight(){
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








