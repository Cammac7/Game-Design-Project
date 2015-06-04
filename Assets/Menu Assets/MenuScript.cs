using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Texture loadingTexture;
	private bool loading = false;
	private Rect screenRect;

	public void StartLevel(string level){
		loading = true;

		Application.LoadLevel(level);
	}

	public void Quit(){
		Application.Quit();
	}

	void OnGUI(){

		if (loading){
			//Debug.Log (Screen.width);
			Debug.Log ("Loading " + loadingTexture.ToString() + " in Rect " + screenRect.ToString());
			screenRect = new Rect(0,0,Screen.width, Screen.height);
			GUI.DrawTexture(screenRect, loadingTexture);
		}
	}
}
