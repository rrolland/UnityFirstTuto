﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Parallx scrolling script that should be assigned to a leyer
/// </summary>
public class ScrollingScript : MonoBehaviour {

    //Speed
    public Vector2 speed = new Vector2(10, 10);

    //direction
    public Vector2 direction = new Vector2(-1, 0);

    //movement linked to camera
    public bool isLinkedToCamera = false;

    public bool isLooping = false;

    private List<Transform> backgroundPart;

	// Use this for initialization
	void Start () {
        if (isLooping)
        {
            // Get all the children of the layer with a renderer
            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                // Add only the visible children
                if (child.GetComponent<Renderer>() != null)
                {
                    backgroundPart.Add(child);
                }
            }

            // Sort by position.
            // Note: Get the children from left to right.
            // We would need to add a few conditions to handle
            // all the possible scrolling directions.
            backgroundPart = backgroundPart.OrderBy(
              t => t.position.x
            ).ToList();
        }
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 movement = new Vector3(
            speed.x * direction.x,
            speed.y * direction.y,
            0
            );

        movement *= Time.deltaTime;
        transform.Translate(movement);

        //move camera
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        // 4 - Loop
        if (isLooping)
        {
            // Get the first object.
            // The list is ordered from left (x position) to right.
            Transform firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                // Check if the child is already (partly) before the camera.
                // We test the position first because the IsVisibleFrom
                // method is a bit heavier to execute.
                if (firstChild.position.x < Camera.main.transform.position.x)
                {
                    // If the child is already on the left of the camera,
                    // we test if it's completely outside and needs to be
                    // recycled.
                    if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                    {
                        // Get the last child position.
                        Transform lastChild = backgroundPart.LastOrDefault();
                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                        // Set the position of the recyled one to be AFTER
                        // the last child.
                        // Note: Only work for horizontal scrolling currently.
                        firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                        // Set the recycled child to the last position
                        // of the backgroundPart list.
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
	}
}
