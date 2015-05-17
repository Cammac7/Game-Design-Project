using UnityEngine;
using System.Collections;

public class BackgroundCycler : MonoBehaviour {

    const float WIDTH = 22;

    void OnCollisionEnter(Collision c)
    {
        GameObject otherObject = c.gameObject;
        if (otherObject.tag == "Background")
        {
            otherObject.transform.position.Set(otherObject.transform.position.x + WIDTH, otherObject.transform.position.y, otherObject.transform.position.z);
        }
    }
}
