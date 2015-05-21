using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

    public static WorldManager instance;

	// Use this for initialization
	void Start () {
        instance = this;
        enabled = false;

        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void GameStart() {
        enabled = true;
    }

    void GameOver() {
        enabled = false;
    }
}
