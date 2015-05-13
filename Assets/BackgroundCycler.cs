using UnityEngine;
using System.Collections;

public class BackgroundCycler : MonoBehaviour {

	const float WIDTH = 22;

<<<<<<< HEAD
	void OnTriggerEnter2D(Collider2D c){
		Debug.Log("Collision!");
		GameObject otherObject = c.gameObject;
		if (otherObject.tag == "Background"){
			otherObject.transform.position += Vector3.right * WIDTH * NUM_PIECES;

=======
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision c){
		GameObject otherObject = c.gameObject;
		if (otherObject.tag == "Background"){
			otherObject.transform.position.Set(otherObject.transform.position.x + WIDTH, otherObject.transform.position.y, otherObject.transform.position.z);
>>>>>>> origin/dev
		}
	}
}
