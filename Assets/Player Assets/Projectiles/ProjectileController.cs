using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour {

    public enum ProjectileType
    {
        Small,
        Medium,
        Large,
        Super,
        Smallball,
        Largeball
    }

    public ProjectileType projectileType = ProjectileType.Small;

    private bool firing = false;
    public int speed;
    private int direction;
    private Vector3 startingLoc;

    public int damage = 1;
    public int experience = 10;

	// Update is called once per frame
	void Update () {
        if (firing)
        {
            transform.position += new Vector3(speed * direction, 0, 0) * Time.deltaTime;

            if (Vector3.Distance(transform.position, startingLoc) > 10.0f)
                KillProjectile();

        }
	}

    public void Fire(Vector3 loc, int dir, float xOffset)
    {
        //offsets here are so that it appear to come directly out of the mouth of the turtle
        transform.position = new Vector3(loc.x + (xOffset * dir)-0.9f, loc.y + 0.55f, loc.z);
        startingLoc = loc;
        direction = dir;
        firing = true;

        if (dir == 1)
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == "Enemy" ||
		    collider.gameObject.tag == "Boss")
        {
            //damage enemy
            collider.gameObject.GetComponent<EnemyController>().Hit(damage);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AddExperience(experience);
            SpecialEffectsHelper.Instance.SparkHit(transform.position + new Vector3((GetComponent<SpriteRenderer>().bounds.size.x / 2), 0, 0));
            KillProjectile();
        }
    }
}
