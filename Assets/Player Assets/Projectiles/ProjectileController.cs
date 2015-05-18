using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    private bool firing = false;
    public int speed;
    private int direction;
    private Vector3 startingLoc;
	
	// Update is called once per frame
	void Update () {
        if (firing)
        {
            transform.position += new Vector3(speed * direction, 0, 0) * Time.deltaTime;

            if (Vector3.Distance(transform.position, startingLoc) > 10.0f)
                KillProjectile();

        }
	}

    public void Fire(Vector3 loc, int dir)
    {
        transform.position = loc;
        startingLoc = loc;
        direction = dir;
        firing = true;

        if (dir == -1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void KillProjectile()
    {
        Destroy(gameObject);
    }
}
