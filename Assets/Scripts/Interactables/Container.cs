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
public class Container : Interactable
{
    //
    [LabelOverride("")] [Tooltip("")]
    public GameObject[] m_agItems;

    //
    [LabelOverride("")] [Tooltip("")]
    public Transform m_tItemSpawn1;

    //
    [LabelOverride("")] [Tooltip("")]
    public Transform m_tItemSpawn2;

    //
    [LabelOverride("")] [Tooltip("")]
    public Transform m_tItemSpawn3;

    //
    private Transform[] m_atSpawnPoints = new Transform[3];

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    new void Awake()
    {
        // Run the base awake
        base.Awake();

        // Set each of the spawn point for items in this container
        m_atSpawnPoints[0] = m_tItemSpawn1.transform;
        m_atSpawnPoints[1] = m_tItemSpawn2.transform;
        m_atSpawnPoints[2] = m_tItemSpawn3.transform;
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    private void Update()
    {
        // leave here for now
    }

    //--------------------------------------------------------------------------------------
    // InteractedWith: override function from base class for what Interactable objects do 
    // once they have been interacted with.
    //--------------------------------------------------------------------------------------
    protected override void InteractedWith()
    {
        // Run the base interactedWith function.
        base.InteractedWith();

        // for each gameobject in the items array
        foreach (GameObject gItem in m_agItems)
        {
            // select a random number from 3
            int randomIndex = Random.Range(0, 3);
            Transform RandomSpawn = m_atSpawnPoints[randomIndex];

            // Instantiate container item at randon location
            Instantiate(gItem, RandomSpawn);
        }
    }
}