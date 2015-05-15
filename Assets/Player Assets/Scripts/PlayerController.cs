using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed;

    private Vector3 speedHorzVector, speedVertVector;
    private bool isRight;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        speedHorzVector = new Vector3(speed, 0, 0);
        speedVertVector = new Vector3(0, speed, 0);
        isRight = true;
    }

    // Update is called once per frame
    void Update()
    {

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0)
        {
            animator.SetInteger("Walk", 1);
            transform.position += speedVertVector * vertical * Time.deltaTime;
        }
        else if (horizontal != 0)
        {
            animator.SetInteger("Walk", 1);
            transform.position += speedHorzVector * horizontal * Time.deltaTime;

            if (horizontal < 0 && isRight || horizontal > 0 && !isRight)
            {
                isRight = !isRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
        else
        {
            animator.SetBool("Stand", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Shoot", true); //start firing animation
        }
    }
}
