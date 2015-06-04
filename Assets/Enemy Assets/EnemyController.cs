using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int health = 2;

    PlayerController player;
    Transform playerTransform;
    Transform thisTransform;
 
    public int hitAmount = 10;

    public float moveSpeed = 2;

    private bool colliding = false;

    private AudioSource hitSound;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hitSound = GetComponent<AudioSource>();
        playerTransform = player.transform;
        thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {

        //only move if not colliding
        if (!colliding)
        {
            Vector3 lookDir = playerTransform.position - thisTransform.position;
            lookDir.Normalize();

            //move towards the player
            thisTransform.position += lookDir * moveSpeed * Time.deltaTime;
        }
	}

    public void Hit(int damage)
    {
        //take damage
        hitSound.Play();
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
            DamagePlayer();
        }
    }

    void DamagePlayer()
    {
        player.Hit(hitAmount);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (!colliding)
            {
                colliding = true;
                //damage player over time
                InvokeRepeating("DamagePlayer", 1f, 1.5f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        CancelInvoke();
        colliding = false;   
    }
}
