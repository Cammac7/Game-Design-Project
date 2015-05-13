using UnityEngine;
using System.Collections;

public class BackgroundCycler : MonoBehaviour {

	const float WIDTH = 22;
	const float NUM_PIECES = 4;

	void OnTriggerEnter2D(Collider2D c){
		Debug.Log ("Collision!");
		GameObject otherObject = c.gameObject;
		Debug.Log (otherObject.ToString());
		if (otherObject.tag == "Background"){
			otherObject.transform.position += Vector3.right * WIDTH * NUM_PIECES;
			Debug.Log("Background Cycled!");
		}
	}
}
