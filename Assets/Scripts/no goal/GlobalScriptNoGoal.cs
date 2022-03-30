using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScriptNoGoal : MonoBehaviour
{
    [SerializeField]
    GameObject prefab = null; //prefab used for the boids

    [HideInInspector]
    public GameObject[] allObjects = null; // array for all the boids
    [HideInInspector]
    public static int areaLimit = 70; // the area that the boids will be within (creates a sqaure as such using the int as the measurements)
    [HideInInspector]
    public static Vector3 boidStart = new Vector3(90.0f, 30.0f, -70.0f); // a specific point to create the area around

    static int Amount = 120; // the amount of boids in the flock

    void Start()
    {
        allObjects = new GameObject[Amount]; // set the length of the array to be the amount of boids

        for (int i = 0; i < Amount; i++) //loop the amount of boids
        {
            Vector3 position = new Vector3(Random.Range(boidStart.x -areaLimit, boidStart.x + areaLimit), //creates a vector with each point being a random number between the area limits
                                           Random.Range(boidStart.y -areaLimit, boidStart.y + areaLimit), // this is what creates the sqaure using the area limits around the start position
                                           Random.Range(boidStart.z -areaLimit, boidStart.z + areaLimit));
            allObjects[i] = (GameObject)Instantiate(prefab, position, Quaternion.identity); // create a boid in the created position 
            allObjects[i].GetComponent<FlockScriptNoGoal>().flockManager = this; //set the connection between this boids flock script and this script
        }
    }
}
