using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed;

    private Vector3 speedVector;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        speedVector = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        transform.position += speedVector * horizontal;


        if (vertical != 0)
        {
            animator.SetBool("Stand", false);
            animator.SetBool("Shoot", false);
            animator.SetInteger("Walk", (int)Math.Ceiling(vertical)); // 0 if left, 1 if right
            transform.position += speedVector * vertical;
        }
        else if (horizontal != 0)
        {
            animator.SetBool("Stand", false);
            animator.SetBool("Shoot", false);
            animator.SetInteger("Walk", (int)Math.Ceiling(horizontal));
            transform.position += speedVector * horizontal;
        }
        else
        {
            animator.SetInteger("Walk", -1);
            animator.SetBool("Shoot", false);
            animator.SetBool("Stand", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.GetBool("Shoot");
            //animator.SetInteger("Walk", -1); //stop walking animation
            //animator.SetBool("Stand", false); //stop standing animation
            animator.SetBool("Shoot", true); //start firing animation
        }
    }
}
