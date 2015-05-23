using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
            KillEnemy();
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
