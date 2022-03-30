using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScriptGoal : MonoBehaviour
{
    [SerializeField]
    GameObject prefab = null; //prefab used for the boids
    [SerializeField]
    GameObject goalPosPrefab = null; //prefab used to show the goal position 
    [SerializeField]
    bool ShowGoalPos = false; // a bool to trigger whether to show the goal positon or not 

    [HideInInspector]
    public GameObject[] allObjects = null; // array for all the boids
    [HideInInspector]
    public Vector3 goalPos = Vector3.zero; // the goal position the boids will head towards
    [HideInInspector]
    public static int areaLimit = 15; // the area that the boids will be within (creates a sqaure as such using the int as the measurements)

    static int Amount = 40; // the amount of boids in the flock
 
    void Start()
    {
        allObjects = new GameObject[Amount]; // set the length of the array to be the amount of boids

        for (int i = 0; i < Amount; i++) //loop the amount of boids
        {
            Vector3 position = new Vector3(Random.Range(-areaLimit, areaLimit), //creates a vector with each position being a random number between the area limits
                                           Random.Range(-areaLimit, areaLimit), // this is what creates the sqaure using the area limits
                                           Random.Range(-areaLimit, areaLimit));
            allObjects[i] = (GameObject)Instantiate(prefab, position, Quaternion.identity); // create a boid in the created position 
            allObjects[i].GetComponent<FlockScriptGoal>().flockManager = this; //set the connection between this boids flock script and this script
        }
    }

    void Update()
    {
        GoalPosRandom();
        GoalPosCheck();
    }

    private void GoalPosCheck()
    {
        if(ShowGoalPos) //if told to show goal
        {
            goalPosPrefab.transform.position = goalPos; // set the prefabs position to the actual goal position
            goalPosPrefab.SetActive(true);  //set it to show
        }
        else if(ShowGoalPos == false)
        {
            goalPosPrefab.SetActive(false); // set the prefab to not show
        }
    }

    private void GoalPosRandom()
    {
        if (Random.Range(0, 10000) < 50) // in a 10000 chance of occuring
        {
            goalPos = new Vector3(Random.Range(-areaLimit, areaLimit), //setting the goal to change to a random position within the area limits
                                  Random.Range(-areaLimit, areaLimit),
                                  Random.Range(-areaLimit, areaLimit));
        }
    }
}
