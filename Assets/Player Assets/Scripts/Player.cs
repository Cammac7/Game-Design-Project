using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Sprite start_sprite;
    public Sprite walk_sprite;

    private bool invokedAnimation;
    private bool on_start_sprite;

	// Use this for initialization
	void Start () {
        invokedAnimation = false;
        on_start_sprite = true;
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
	}

    void TurtleWalk()
    {
        GetComponent<SpriteRenderer>().sprite = 
            (on_start_sprite) ? walk_sprite : start_sprite;

        on_start_sprite = !on_start_sprite;

    }
}
