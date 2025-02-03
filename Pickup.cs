using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public BoxCollider2D coll;
    public Transform player, gunContainer;

    public float pickUpRange;

    public bool equipped = false;
    public static bool slotFull;
    public Pickaxe toolScript;

    private void Start()
    {
        toolScript = GetComponent<Pickaxe>();
        //Setup
        if (!equipped)
        {
            toolScript.enabled = false;
            coll.isTrigger = false;
            slotFull = false;
        }
        if (equipped)
        {
            toolScript.enabled = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        coll.isTrigger = true;

        //Enable script
        toolScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        coll.isTrigger = false;

        //Disable script
        toolScript.enabled = false;
    }
}