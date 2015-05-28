using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public void StartLevel(string level){
		Application.LoadLevel(level);
	}

	public void Quit(){
		Application.Quit();
	}
}
