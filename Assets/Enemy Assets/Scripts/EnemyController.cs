using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int health = 2;

    PlayerController player;
    Transform playerTransform;
    Transform thisTransform;

    public int hitAmount = 10;
	public GameObject endgame;
	private bool edirectionIsRight;
	private float previousposition;
    public float moveSpeed = 2;
    private bool colliding = false;
    private AudioSource hitSound;

	private bool headAtPlayer = false;
	private int tooFar = 0;
	private int randomInterval;

	public GameObject winText;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hitSound = GetComponent<AudioSource>();
        playerTransform = player.transform;
        thisTransform = transform;
		previousposition = thisTransform.position.x;
		edirectionIsRight = true;
		randomInterval = Random.Range (0, 10);

		InvokeRepeating ("CheckIfTooFar", 0, 1); //start immediate, check every second
	}

	void CheckIfTooFar()
	{
		tooFar++;

		if (tooFar == 3 + randomInterval) {
			headAtPlayer = true;
			CancelInvoke();
		}
	}

	// Update is called once per frame
	void Update () {
        //only move if not colliding and the player is not dead
        if (!colliding && !PlayerController.Death)
        {
            Vector3 lookDir = playerTransform.position - thisTransform.position;
			if (Vector3.Distance(playerTransform.position, thisTransform.position) <= 5f)
				headAtPlayer = true;

			if (!headAtPlayer)
			{
				lookDir.y = 0;
			}
            lookDir.Normalize();
            //move towards the player
            thisTransform.position += lookDir * moveSpeed * Time.deltaTime;
        }

		eCheckDirection();
		previousposition = thisTransform.position.x;
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
        if (GetComponent<EnemyManager>() != null) { //we are a beehive
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayBeehiveDeath();
        }

		if (this.tag == "Boss") {
			//end the game here....
			PlayerController.Death = true;
            GameObject.FindGameObjectWithTag("LevelMusic").GetComponent<AudioSource>().Stop();
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkingSource.isPlaying)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkingSource.Stop();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WinGame.transform.position =
                new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y - 3f, 0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WinGame.gameObject.SetActive(true);
		}

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
        if (!PlayerController.Death)
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
	private void eCheckDirection()
	{
		float currentPosition = thisTransform.position.x;
		if ((currentPosition < previousposition)&&(edirectionIsRight)) {
			Vector3 theeScale = thisTransform.localScale;
			theeScale.x *= -1;
			thisTransform.localScale = theeScale;
			edirectionIsRight = false;
		}
		if((currentPosition > previousposition)&&(!edirectionIsRight))
		{
			Vector3 theeScale = thisTransform.localScale;
			theeScale.x *= -1;
			thisTransform.localScale = theeScale;
			edirectionIsRight = true;
		}
	}
}
