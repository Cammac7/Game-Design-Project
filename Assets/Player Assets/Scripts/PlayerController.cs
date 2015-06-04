using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int health = 100;
    public int experience = 0;
	public int xpLevelReached = 100;
	public Slider healthSlider;
    public Slider experienceSlider;

    public AudioSource levelUpSource;
    public AudioSource fireShootSource;
    public AudioSource[] walkingSource;

	public Transform smallProjectileFire, mediumProjectileFire, largeProjectileFire, superProjectileFire;
	public Transform smallProjectileFireball, largeProjectileFireball;

	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

	public string whichProjectile = "Small";
	bool damaged;

    public const int RIGHT = 1;
    public const int LEFT = -1;

	private Animator animator;

    public bool directionIsRight;

	private bool atTop = false;
	private bool atBottom = false;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        directionIsRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var speedVector = new Vector3(speed * horizontal, speed * vertical, 0);

		if (atTop && speedVector.y > 0){
			speedVector.y = 0;
		}
		
		if (atBottom && speedVector.y < 0){
			speedVector.y = 0;
		}

		Vector3 newPosition = (speedVector * Time.deltaTime) + transform.position;


		transform.position = newPosition;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            ChangeState("Walking");

            //play walking audio
            walkingSource[UnityEngine.Random.Range(0, 3)].Play();

            ScalePlayer();

        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            ChangeState("Walking");

            //play walking audio
            walkingSource[UnityEngine.Random.Range(0, 3)].Play();

            //flip the sprite if we change direction
            CheckDirection();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
                 Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            ChangeState("Standing");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeState("End Shooting");
        }

        CheckWeaponChange();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator.GetBool("Shoot"))
            {
                Transform projectile = Instantiate(GetCurrentProjectile());
                SetDamageAmount(projectile);

                fireShootSource.Play();

                projectile.
                    GetComponent<ProjectileController>().Fire(transform.position,
                                                              (directionIsRight ? PlayerController.RIGHT : PlayerController.LEFT),
                                                               GetProjectileXOffset());
            }

            ChangeState("Shooting");
        }

        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    private void CheckWeaponChange()
    {
        string current = whichProjectile;

        if (Input.GetKey(KeyCode.Alpha1))
            whichProjectile = "Small";
        if (Input.GetKey(KeyCode.Alpha2))
            whichProjectile = "Medium";
        if (Input.GetKey(KeyCode.Alpha3))
            whichProjectile = "Large";
        if (Input.GetKey(KeyCode.Alpha4))
            whichProjectile = "Super";
        if (Input.GetKey(KeyCode.Alpha5))
            whichProjectile = "SmallBall";
        if (Input.GetKey(KeyCode.Alpha6))
            whichProjectile = "LargeBall";

        if (current != whichProjectile)
        {
            //ChangeState("Level Up");
            
        }
    }

    public Transform GetCurrentProjectile()
    {
        switch (whichProjectile)
        {
            case "Medium":
                return mediumProjectileFire;
            case "Large":
                return largeProjectileFire;
            case "Super":
                return superProjectileFire;
            case "SmallBall":
                return smallProjectileFireball;
            case "LargeBall":
                return largeProjectileFireball;
        }

        return smallProjectileFire;
    }

    public float GetProjectileXOffset()
    {
        if (whichProjectile == "Small" || whichProjectile == "Medium" ||
            whichProjectile == "Large" || whichProjectile == "Super")
            return 3.30f;

        return 1.28f; // small ball and large ball
    }

    public void SetDamageAmount(Transform projectile)
    {
        int damage = 1;

        switch (whichProjectile)
        {
            case "Small":
                damage = 1;
                break;
            case "Medium":
                damage = 3;
                break;
            case "Large":
                damage = 5;
                break;
            case "Super":
                damage = 10;
                break;
            case "SmallBall":
                damage = 7;
                break;
            case "LargeBall":
                damage = 9;
                break;
        }

        projectile.GetComponent<ProjectileController>().damage = damage; //set damage somewhere around here
    }

    private void ChangeState(string state)
    {
        switch(state)
        {
            case "Shooting":
                animator.SetBool("Stand", false);
                animator.SetBool("Shoot", true); //start firing animation
                break;
            case "Standing":
                animator.SetBool("Shoot", false);
                animator.SetBool("Walk", false);
                animator.SetBool("Stand", true);
                break;
            case "Walking":
                animator.SetBool("Stand", false);
                animator.SetBool("Walk", true);
                break;
            case "Level Up":
                animator.SetBool("Shoot", false);
                animator.SetBool("Walk", false);
                animator.SetBool("Stand", false);
                animator.SetBool("Level Up", true);
                break;
        }
    }

    private void CheckDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && directionIsRight ||
            Input.GetKey(KeyCode.RightArrow) && !directionIsRight)
        {
            directionIsRight = !directionIsRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void Hit(int damage)
    {
        //take damage
        damaged = true;
        health -= damage;
        healthSlider.value = health;
        
        if (health <= 0)
        {
            //handle death here, possible restart screen
        }
    }

    public void AddExperience(int xp)
    {
        experience += xp;
		Debug.Log (experience);
        experienceSlider.value = experience;
		Debug.Log (experienceSlider.value);
        if (experienceSlider.value >= xpLevelReached)
        {
            experienceSlider.value = 0;
			experience = 0;
			Debug.Log (experienceSlider.value);
            ChangeState("Level Up");
            levelUpSource.Play();
			experienceSlider.maxValue = (xpLevelReached*3);
			xpLevelReached = (xpLevelReached*3);
        }
    }

    private void ScalePlayer()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
			if (atTop) {
				return;
			}

            Vector3 scale = transform.localScale;
            if (scale.x < 0)
                scale.x = Math.Min(scale.x + 0.002f, -0.4f);
            else
                scale.x = Math.Max(scale.x - 0.002f, 0.4f);

            scale.y = Math.Max(scale.y - 0.002f, 0.4f);
            transform.localScale = scale;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
			if (atBottom){
				return;
			}

            Vector3 scale = transform.localScale;
            if (scale.x < 0)
                scale.x = Math.Max(scale.x - 0.002f, -1.4f);
            else
                scale.x = Math.Min(scale.x + 0.002f, 1.4f);

            scale.y = Math.Min(scale.y + 0.002f, 1.4f);
            transform.localScale = scale;
        }
    }

	void OnTriggerEnter2D(Collider2D c){
		if (c.tag == "Wall"){
			if (c.name == "Top Boundary"){
				atTop = true;
			}
			else {
				atBottom = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.gameObject.tag == "Wall"){
			if (atTop){
				atTop = false;
			}

			if (atBottom){
				atBottom = false;
			}
		}

	}
}
