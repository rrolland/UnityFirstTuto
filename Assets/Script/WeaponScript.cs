using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

    //variables


    // Projectile prefab
    public Transform shotPrefab;

    //Cooldown in sec between 2 shots
    public float shootingRate = 0.25f;

    private float shootCooldown;


	// Use this for initialization
	void Start () {
        shootCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;

        }
	}

    //Shooting from another script


    //create a new projectile

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            //create new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            //assign position
            shotTransform.position = transform.position;

            //is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make weapon shot always toward it
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }

        }
    }

    //Is weapon ready to create projectile

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
