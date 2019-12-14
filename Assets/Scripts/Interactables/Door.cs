//--------------------------------------------------------------------------------------
// Purpose: 
//
// Description: 
//
// Author: Thomas Wiltshire
//--------------------------------------------------------------------------------------

// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// Door object. Inheriting from Interactable.
//--------------------------------------------------------------------------------------
public class Door : Interactable
{
    //
    BoxCollider2D[] m_bcColliders;

    //
    private Vector2 m_v2TriggerOffset;

    //
    private Vector2 m_v2TriggerSize;

    //
    private bool m_bLatchable;

    //
    public Vector2 m_v2LatchOffset = new Vector2(0,0);

    //
    public Vector2 m_v2LatchSize = new Vector2(0,0);



    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    new void Awake()
    {
        // Run the base awake
        base.Awake();

        //
        m_bcColliders = GetComponents<BoxCollider2D>();

        //
        foreach (BoxCollider2D bcCollider2D in m_bcColliders)
        {
            //
            if (bcCollider2D.isTrigger)
            {
                //
                m_v2TriggerOffset = bcCollider2D.offset;

                //
                m_v2TriggerSize = bcCollider2D.size;
            }
        }

        //
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    private void Update()
    {
    }

    //--------------------------------------------------------------------------------------
    // InteractedWith: override function from base class for what Interactable objects do 
    // once they have been interacted with.
    //--------------------------------------------------------------------------------------
    protected override void InteractedWith()
    {
        // Run the base interactedWith function.
        base.InteractedWith();

        //
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        //
        foreach (BoxCollider2D bcCollider2D in m_bcColliders)
        {
            //
            if (bcCollider2D.isTrigger)
            {
                //
                bcCollider2D.offset = m_v2LatchOffset;
                bcCollider2D.size = m_v2LatchSize;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // OnTriggerEnter: OnTriggerEnter is called when the Collider cObject enters the trigger.
    //
    // Param:
    //      cObject: The other Collider invloved in the collision.
    //--------------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D cObject)
    {
        // if collides is player and not interacted or interactable
        if (cObject.tag == "Latch" && m_bLatchable)
        {
            // turn off interaction and set door to closed
            m_bInteracted = false;
            m_bInteractable = false;

            // stop the door from moving
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            //
            foreach (BoxCollider2D bcCollider2D in m_bcColliders)
            {
                //
                if (bcCollider2D.isTrigger)
                {
                    //
                    bcCollider2D.offset = m_v2TriggerOffset;
                    bcCollider2D.size = m_v2TriggerSize;
                }
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // OnTriggerExit: OnTriggerExit is called when the Collider cObject exits the trigger.
    //
    // Param:
    //      cObject: The other Collider invloved in the collision.
    //--------------------------------------------------------------------------------------
    private void OnTriggerExit2D(Collider2D cObject)
    {
        //
        if (cObject.tag == "Latch")
        {
            m_bLatchable = true;
        }
    }
}