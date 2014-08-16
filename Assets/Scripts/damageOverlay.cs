using UnityEngine;
using System.Collections;

public class damageOverlay : MonoBehaviour {
	public Sprite[] damageStages;
	public int damageStage; //0 = not broken; 1 = barely broken; 2 = a bit broken; 3 = more than halfway broken; 4 = almost broken;
	int currentDamageStage;
	void Start () {
		currentDamageStage = damageStage;
	}

	void Update () {
		if(currentDamageStage != damageStage){
			gameObject.GetComponent<SpriteRenderer>().sprite = damageStages[damageStage];
			currentDamageStage = damageStage;
		}
	}
}
