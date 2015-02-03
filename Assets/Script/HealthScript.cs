using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public int hp = 1;

    public bool isEnemy = true;

    public void damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //is this a shot ?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();

        if (shot != null)
        {
            //Avoid firendly fire
            if (shot.isEnemyShot != isEnemy)
            {
                damage(shot.damage);

                //destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
