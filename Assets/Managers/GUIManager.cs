using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public Transform GUI;
    public GUIText titleText;

    private static GUIManager instance;
    public static GUIManager Instance
    {
        get { return instance; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
