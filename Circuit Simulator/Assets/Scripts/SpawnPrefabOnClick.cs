using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabOnClick : MonoBehaviour
{
    public GameObject prefabToSpawn; // the prefab to spawn
    public GameObject objectToConnect; // the game object to connect the spawned prefab to

    private bool hasSpawned = false; // flag to track if the prefab has already been spawned

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position to the game world
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object clicked on is the object to connect the prefab to
                if (hit.collider.gameObject == objectToConnect)
                {
                    if (!hasSpawned)
                    {
                        // Spawn the prefab and set its position to the object to connect
                        GameObject newObject = Instantiate(prefabToSpawn);
                        newObject.transform.position = objectToConnect.transform.position;

                        // Connect the spawned prefab to the object to connect
                        newObject.transform.parent = objectToConnect.transform;

                        // Set the flag to indicate that the prefab has been spawned
                        hasSpawned = true;
                    }
                }
            }
        }
    }
}
