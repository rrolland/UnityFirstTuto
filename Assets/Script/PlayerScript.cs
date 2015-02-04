using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//Speed of ship
	public Vector2 speed = new Vector2 (50, 50);
	
	private Vector2 movement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//retrive axis info
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		//Movement per direction
		movement = new Vector2 (
			speed.x * inputX,
			speed.y * inputY);

        // Shooting
        bool shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                //False because player is not enemy
                weapon.Attack(false);
            }
        }

        // 6 - Make sure we are not outside the camera bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );

	}

	void FixedUpdate()
	{
		//move game object
		rigidbody2D.velocity = movement;
	}

    void OnDestroy()
    {
        // Game Over.
        // Add the script to the parent because the current game
        // object is likely going to be destroyed immediately.
        transform.parent.gameObject.AddComponent<GameOverScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        //Collision with enemy
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();

        if (enemy != null)
        {
            // Kill enemy
            HealthScript enemyHealth = enemy.gameObject.GetComponent<HealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.damage(enemyHealth.hp);
            }

            damagePlayer = true;
            
        }

        //damage player

        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                Debug.Log("damage collision");
                playerHealth.damage(1);
            }
        }
        
    }

}
