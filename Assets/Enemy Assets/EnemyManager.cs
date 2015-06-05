using UnityEngine;
using System.Collections;


public class EnemyManager : MonoBehaviour {

	public GameObject Enemy;
	public float spawnTime = 3f;
	public int numSpawns = 5;
	public Transform[] spawnPoints;

	private int alreadySpawned = 0;

	// Use this for initialization
	void Start () {
		while (alreadySpawned < numSpawns){
			InvokeRepeating ("Spawn", spawnTime, spawnTime);
		}
	}
	
	// Update is called once per frame
	void Spawn () {
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (Enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}
}
