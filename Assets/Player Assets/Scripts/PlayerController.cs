using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed;

    public int health = 100;

    public AudioSource levelUpSource;
    private AudioClip levelUpClip;

    private const int RIGHT = 1;
    private const int LEFT = -1;

    public Transform smallProjectileFire, mediumProjectileFire, largeProjectileFire, superProjectileFire;
    public Transform smallProjectileFireball, largeProjectileFireball;

    private bool directionIsRight;

    string whichProjectile = "Small";

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

        var newPositionVector = new Vector3(speed * horizontal, speed * vertical, 0);
        transform.position += newPositionVector * Time.deltaTime;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            ChangeState("Walking");
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            ChangeState("Walking");

            //flip the sprite if we change direction
            CheckDirection();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
                 Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                 Input.GetKeyUp(KeyCode.Space))
        {
            ChangeState("Standing");
        }

        CheckWeaponChange();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState("Shooting");

            Transform projectile = Instantiate(GetCurrentProjectile());
            SetDamageAmount(projectile);

            projectile.GetComponent<ProjectileController>().Fire(transform.position, (directionIsRight ? RIGHT : LEFT));
        }
    }

    private void CheckWeaponChange()
    {
        string current = whichProjectile;

        if (Input.GetKey(KeyCode.Alpha1))
            whichProjectile = "Small";
        if (Input.GetKey(KeyCode.Alpha2))
            whichProjectile = "Large";
        if (Input.GetKey(KeyCode.Alpha3))
            whichProjectile = "Super";
        if (Input.GetKey(KeyCode.Alpha4))
            whichProjectile = "SmallBall";
        if (Input.GetKey(KeyCode.Alpha5))
            whichProjectile = "LargeBall";

        if (current != whichProjectile)
        {
            levelUpSource.Play();
        }
    }

    private Transform GetCurrentProjectile()
    {
        switch (whichProjectile)
        { 
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

    private void SetDamageAmount(Transform projectile)
    {
        int damage = 1;

        switch (whichProjectile)
        {
            case "Small":
                damage = 1;
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

    public void Hit(int damage)
    {
        health -= damage;
    }
}
