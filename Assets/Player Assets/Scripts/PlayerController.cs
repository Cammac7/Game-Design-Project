using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public float speed;

    private bool isRight;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        isRight = true;
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
            int direction = (Input.GetKeyDown(KeyCode.UpArrow) ? 1 : -1);

            animator.SetBool("Stand", false);
            animator.SetBool("Walk", true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            int direction = (Input.GetKeyDown(KeyCode.RightArrow) ? 1 : -1);

            animator.SetBool("Stand", false);
            animator.SetBool("Walk", true);
            
            if (Input.GetKeyDown(KeyCode.LeftArrow) && isRight || 
                Input.GetKeyDown(KeyCode.RightArrow) && !isRight)
            {
                isRight = !isRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
                 Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                 Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Shoot", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Stand", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Stand", false);
            animator.SetBool("Shoot", true); //start firing animation
        }
    }
}
