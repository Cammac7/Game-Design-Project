using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed;

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

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeState("Walking");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
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

            Transform currentProjectile = GetCurrentProjectile();       

            //only shoots small for now?
            Transform projectile = Instantiate(currentProjectile);

            projectile.GetComponent<ProjectileController>().Fire(transform.position, (directionIsRight ? RIGHT : LEFT));
        }
    }

    private void CheckWeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            whichProjectile = "Small";
        if (Input.GetKeyDown(KeyCode.Alpha2))
            whichProjectile = "Large";
        if (Input.GetKeyDown(KeyCode.Alpha3))
            whichProjectile = "Super";
        if (Input.GetKeyDown(KeyCode.Alpha4))
            whichProjectile = "SmallBall";
        if (Input.GetKeyDown(KeyCode.Alpha5))
            whichProjectile = "LargeBall";
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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && directionIsRight ||
            Input.GetKeyDown(KeyCode.RightArrow) && !directionIsRight)
        {
            directionIsRight = !directionIsRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    
}
