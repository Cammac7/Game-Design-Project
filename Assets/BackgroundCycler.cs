using UnityEngine;
using System.Collections;

public class BackgroundCycler : MonoBehaviour {

    const float WIDTH = 22;
	const int NUM_TILES = 4;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
		
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        GameObject otherObject = c.gameObject;
        if (otherObject.tag == "Background")
        {
			otherObject.transform.position += new Vector3(WIDTH * NUM_TILES, 0, 0);
        }
    }
}
