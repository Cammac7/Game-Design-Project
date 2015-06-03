using UnityEngine;
using System.Collections;

public class BackgroundCycler : MonoBehaviour
{

    const float WIDTH = 22;
    const int NUM_TILES = 4;

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Trigger entered!");
        GameObject otherObject = c.gameObject;
        if (otherObject.tag == "Background")
        {
            otherObject.transform.position += new Vector3(WIDTH * NUM_TILES, 0, 0);
            Debug.Log("Object moved!");
        }
    }
}