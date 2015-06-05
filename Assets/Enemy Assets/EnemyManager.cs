using UnityEngine;
using System.Collections;


public class EnemyManager : MonoBehaviour {

	public GameObject Enemy;
	public float spawnTime = 3f;
	public int numSpawns = 5;

	private int spawnedAlready;
	private float timeOfLastSpawn;

	void Update(){
		if (spawnedAlready < numSpawns && Time.time > (timeOfLastSpawn + spawnTime)){
			timeOfLastSpawn = Time.time;
			spawnedAlready++;
			Spawn();
		}
	}

	void Spawn () {
		Instantiate (Enemy, transform.position, transform.rotation);
}
}
