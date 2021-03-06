﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_Collision : NetworkBehaviour
{

    public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter2D(Collision2D coll)
    /* Triggered when the player's collider comes into contact with any other
    collider. Takes in the argument coll to refer to the other object that entered the collision.

    If the other object has the tag Projectile, the other object is destroyed. Also the player's health will be decreased

    @rtype : None

    */
    {
        if (coll.gameObject.tag == "Projectile")
        {
            CmdTakeDamage(coll.gameObject);
            CmdDestroyProjectile(coll.gameObject);
        }


    }

    [Command]
    void CmdTakeDamage(GameObject projectile)
    {
        /* Takes in the player gameObject and the projectile gameObject as an argument to deal damage to the player
        If the player's health drops to zero or below, the player gmaeObject is destroyed by the server

        @rtype : None
        */
        health -= 1;
        // health -= projectile......damage
        if (health <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    [Command]
    void CmdDestroyProjectile(GameObject projectile)
    /* Destroy the projectile on the server.

   @rtype : None

   */
    {
        NetworkServer.Destroy(projectile);

        // May also need to remove any RPCs associated with the projectile
    }


    public bool CheckWallCollision(Vector3 movementDirection)

        /*
            Checks if a wall is in the direction of movement, and then returns true if a wall is found, false if no wall is found.

            @type movementDirection: Vector3
            @rtype: bool

        */ 
    {
        if (Physics2D.Raycast(transform.position,transform.position - movementDirection, 1f,11)){
            return true;

        }
        else
        {
            return false;
        }
    }
}
