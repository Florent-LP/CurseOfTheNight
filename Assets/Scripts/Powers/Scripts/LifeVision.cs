﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace Powers
{
    public class LifeVision : Power
    {
        // User settings
        public float maxDuration = 10f;

        // Materials
        protected Dictionary<int, Material[]> originalMaterials = new Dictionary<int, Material[]>();
        [SerializeField]
        protected Material alternativeMaterial;


        // State variables
        protected enum State
        {
            disabled,
            enabled,
            cancelling
        }
        protected State currentState = State.disabled;

        // Use this for initialization
        protected void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            if (powerEnabled)
            {
                string cancelKey = useTriggerKeyOnly ? manager._triggerKey : manager._cancelKey;

                // Trigger
                if (CrossPlatformInputManager.GetButtonDown(manager._triggerKey) && currentState == State.disabled)
                {
                    if (manager.RequestMana(manaCost))
                        manager.StartCoroutine(ExecuteVision());
                    else
                        currentState = State.disabled;
                }
                // Cancel
                else if (CrossPlatformInputManager.GetButtonDown(cancelKey) && currentState == State.enabled)
                    currentState = State.cancelling;
            }
        }

        // Fixed update is called in sync with physics
        protected void FixedUpdate()
        {

        }

        protected IEnumerator ExecuteVision()
        {
            currentState = State.enabled;

            // Step 1 : Enable vision
            // Find all enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                // Backup and replace materials
                foreach (GameObject enemy in enemies) // Browse enemies
                    foreach (SkinnedMeshRenderer enemyRend in enemy.GetComponentsInChildren<SkinnedMeshRenderer>()) // Browse renderers
                    {
                        originalMaterials.Add(enemyRend.GetInstanceID(), enemyRend.materials);
                        enemyRend.materials = new Material[] { alternativeMaterial };
                    }
            }

            // Step 2 : Wait for vision timeout
            //yield return new WaitForSeconds(maxDuration); // version without cancel
            float startTime = Time.time;
            while (Time.time - startTime < maxDuration && currentState != State.cancelling)
                yield return null;

            // Step 3 : Disable vision
            if (originalMaterials.Count > 0)
            {
                // Find all enemies
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length > 0)
                {
                    // Restore altered materials
                    foreach (GameObject enemy in enemies) // Browse enemies
                        foreach (SkinnedMeshRenderer enemyRend in enemy.GetComponentsInChildren<SkinnedMeshRenderer>()) // Browse renderers
                            if (originalMaterials.ContainsKey(enemyRend.GetInstanceID()))
                                enemyRend.materials = originalMaterials[enemyRend.GetInstanceID()];
                    originalMaterials.Clear();
                }
            }

            // Prevent abuse
            yield return new WaitForSeconds(timeBetweenUses);

            currentState = State.disabled;
        }
    }
}