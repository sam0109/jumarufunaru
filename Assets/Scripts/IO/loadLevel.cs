using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class loadLevel : MonoBehaviour {
	public const int chunkSize = 1000;
	public string fileIOVersion = "0.0.1";
	public List<byte[]> mapPos = new List<byte[]>{};
	byte[] currentCol = new byte[chunkSize];
	public FileStream levelStream;

	void Awake () {
		string fileName = Application.persistentDataPath + "/" + Application.loadedLevelName;
		levelStream = new FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None );
		int numBytesToRead = chunkSize;
		int numBytesRead = 0;
		int columnsRead = 0;
		int bytesRead;
		while ((bytesRead = levelStream.Read(currentCol, numBytesRead, numBytesToRead)) > 0) {
			numBytesRead += bytesRead;
			numBytesToRead -= bytesRead;
			if (numBytesRead == chunkSize) {
				columnsRead++;
				numBytesRead = 0;
				numBytesToRead = chunkSize;
				mapPos.Add(currentCol);
				currentCol = new byte[1000];
			}
		}
	}
	
	void OnDestroy(){
		levelStream.Seek(0, SeekOrigin.Begin);
		foreach(byte[] column in mapPos){
			levelStream.Write(column, 0, chunkSize);
		}
		levelStream.Close();
	}
}
