using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int health = 2;

    GameObject player;
 
    public int hitAmount = 10;

    public float moveSpeed = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 lookDir = player.transform.position - transform.position;
        lookDir.Normalize();

        //move towards the player
        transform.position += lookDir * moveSpeed * Time.deltaTime;
	}

    public void Hit(int damage)
    {
        //take damage
        GetComponent<AudioSource>().Play();
        health -= damage;

        if (health <= 0)
            KillEnemy();
    }

    private void KillEnemy()
    {
        SpecialEffectsHelper.Instance.Explosion(transform.position);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            //damage player
            player.GetComponent<PlayerController>().Hit(hitAmount);
        }
    }
}
