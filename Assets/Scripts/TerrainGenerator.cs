using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour
{
		public GameObject player;
		public GameObject ground;
		public float cameraWidth;
		public float cameraHeight;
		public float bufferSize;
		public int generationBottomLimit;
		public float generationYCoordStart;
		public float blockSideLength;
		float rightGenCoords;
		float leftGenCoords;
		float bottomGenCoords;
		// Use this for initialization
		void Start ()
		{
				bottomGenCoords = 0;
				for (float i = bottomGenCoords; i > Camera.main.transform.position.y - (cameraHeight/2) - bufferSize; i-= blockSideLength) {
						GameObject block = (GameObject)Instantiate (ground, new Vector3 (0, generationYCoordStart + i, 0), Quaternion.identity);
						block.transform.parent = gameObject.transform;
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				while (Camera.main.transform.position.x + (cameraWidth/2) + bufferSize > rightGenCoords) {
						rightGenCoords += blockSideLength;
						for (float i = bottomGenCoords; i > Camera.main.transform.position.y - (cameraHeight/2) - bufferSize; i-= blockSideLength) {
								GameObject block = (GameObject)Instantiate (ground, new Vector3 (rightGenCoords, generationYCoordStart + i, 0), Quaternion.identity);
								block.transform.parent = gameObject.transform;
						}
				}
				while (Camera.main.transform.position.x - (cameraWidth/2) - bufferSize < leftGenCoords) {
						leftGenCoords -= blockSideLength;
						for (float i = bottomGenCoords; i > Camera.main.transform.position.y - (cameraHeight/2) - bufferSize; i-= blockSideLength) {
								GameObject block = (GameObject)Instantiate (ground, new Vector3 (leftGenCoords, generationYCoordStart + i, 0), Quaternion.identity);
								block.transform.parent = gameObject.transform;
						}
				}
		}
}
