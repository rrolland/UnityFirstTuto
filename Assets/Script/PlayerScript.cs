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
	}

	void FixedUpdate()
	{
		//move game object
		rigidbody2D.velocity = movement;
	}




}
