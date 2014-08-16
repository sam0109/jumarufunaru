using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {
	public Sprite[] textures;
	public float lightLevel;
	public float opacity;
	buildLevel level;
	public GameObject upBlock;
	public Shadow upBlockShadow;
	public GameObject leftBlock;
	public Shadow leftBlockShadow;
	public GameObject rightBlock;
	public Shadow downBlockShadow;
	public GameObject downBlock;
	public Shadow rightBlockShadow;

	void Start () {
		level = GameObject.Find("Terrain").GetComponent<buildLevel>();

		if(Mathf.RoundToInt(transform.position.x - 1) > 0){
			leftBlock = level.rendered[Mathf.RoundToInt(transform.position.x - 1), Mathf.RoundToInt(transform.position.y)];
			if(leftBlock != null){
				leftBlockShadow = leftBlock.GetComponent<Shadow>();
				leftBlockShadow.rightBlock = gameObject;
				leftBlockShadow.rightBlockShadow = this;
			}
		}
		else{
			leftBlock = null;
		}

		rightBlock = level.rendered[Mathf.RoundToInt(transform.position.x + 1), Mathf.RoundToInt(transform.position.y)];
		if(rightBlock != null){
			rightBlockShadow = rightBlock.GetComponent<Shadow>();
			rightBlockShadow.leftBlock = gameObject;
			rightBlockShadow.leftBlockShadow = this;
		}

		upBlock = level.rendered[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + 1)];
		if(upBlock != null){
			upBlockShadow = upBlock.GetComponent<Shadow>();
			upBlockShadow.downBlock = gameObject;
			upBlockShadow.downBlockShadow = this;
		}

		downBlock = level.rendered[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y - 1)];
		if(downBlock != null){
			downBlockShadow = downBlock.GetComponent<Shadow>();
			downBlockShadow.upBlock = gameObject;
			downBlockShadow.upBlockShadow = this;

		}
	}

	void Update () {

		if(upBlock != null && leftBlock != null && rightBlock != null && downBlock != null){
			lightLevel = Mathf.Max(new float[]{upBlockShadow.lightLevel, downBlockShadow.lightLevel, leftBlockShadow.lightLevel, rightBlockShadow.lightLevel, opacity}) - opacity;
		}
		else{

			if(upBlock != null){
				lightLevel = upBlockShadow.lightLevel - opacity;
			}
			if(downBlock != null){
				lightLevel = Mathf.Max(downBlockShadow.lightLevel - opacity, lightLevel);
			}
			if(rightBlock != null){
				lightLevel = Mathf.Max(rightBlockShadow.lightLevel - opacity, lightLevel);
			}
			if(leftBlock != null){
				lightLevel = Mathf.Max(leftBlockShadow.lightLevel - opacity, lightLevel);
			}
			lightLevel = Mathf.Max(0, lightLevel);
		}
			GetComponent<SpriteRenderer>().sprite = textures[Mathf.FloorToInt(lightLevel)];
	}
}
