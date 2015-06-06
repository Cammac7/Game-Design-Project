using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject Enemy;
	public float spawnTime = 3f;
	public int numSpawns = 5;
	public float spawnDistance = 20;

	private int spawnedAlready;
	private float timeOfLastSpawn;
	private GameObject player;

	void Awake() {
		player = GameObject.Find("Player");
		//Debug.Log((player == null));
	}

	void Update(){
		//Debug.Log((player == null));
		float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		if (spawnedAlready < numSpawns 
		    && Time.time > (timeOfLastSpawn + spawnTime)
		    && distanceToPlayer < spawnDistance
		    ) {

			timeOfLastSpawn = Time.time;
			spawnedAlready++;
			Spawn();
		}
	}

	void Spawn () {
		Instantiate (Enemy, transform.position, transform.rotation);
}
}
