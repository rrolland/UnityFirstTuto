using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    private WeaponScript[] weapons;
    private bool hasSpawn;
    private MoveScript moveScript;

	// Use this for initialization
	void Awake() {
	
        //retrieve weapons
        weapons = GetComponentsInChildren<WeaponScript>();

        // Retrieve scripts to disable when not spawn
        moveScript = GetComponent<MoveScript>();
	}
	
	// Update is called once per frame
    void Update()
    {

        // 2 - Check if the enemy has spawned.
        if (hasSpawn == false)
        {
            if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            foreach (WeaponScript weapon in weapons)
            {
                //auto fire
                if (weapon != null && weapon.CanAttack)
                {
                    weapon.Attack(true);
                }
            }
            // 4 - Out of the camera ? Destroy the game object.
            if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    // 1 - Disable everything
    void Start()
    {
        hasSpawn = false;

        // Disable everything
        // -- collider
        GetComponent<Collider2D>().enabled = false;
        // -- Moving
        moveScript.enabled = false;
        // -- Shooting
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    // 3 - Activate itself.
    private void Spawn()
    {
        hasSpawn = true;

        // Enable everything
        // -- Collider
        GetComponent<Collider2D>().enabled = true;
        // -- Moving
        moveScript.enabled = true;
        // -- Shooting
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}
