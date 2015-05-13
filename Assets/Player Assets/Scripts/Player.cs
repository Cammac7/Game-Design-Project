using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

<<<<<<< HEAD
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update() {
=======
    public Sprite start_sprite;
    public Sprite walk_sprite;
	public float speed;

    private bool invokedAnimation;
    private bool on_start_sprite;
	private Vector3 speedVector;

	// Use this for initialization
	void Start () {
        invokedAnimation = false;
        on_start_sprite = true;
		speedVector = new Vector3(speed, 0 ,0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!invokedAnimation)
                InvokeRepeating("TurtleWalk", 0.0f, 0.3f);

            invokedAnimation = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            invokedAnimation = false;
            CancelInvoke("TurtleWalk");
        }

		if (Input.GetAxis("Horizontal") != 0){
			transform.position += speedVector * Input.GetAxis("Horizontal") * Time.deltaTime;
		}
	}

    void TurtleWalk()
    {
        GetComponent<SpriteRenderer>().sprite = 
            (on_start_sprite) ? walk_sprite : start_sprite;

        on_start_sprite = !on_start_sprite;
>>>>>>> origin/master

    }
}
