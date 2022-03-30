using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockScriptNoGoal : MonoBehaviour
{
    [HideInInspector]
    public GlobalScriptNoGoal flockManager; //creates a connection to the global script

    float speed; // the speed of the boid
    float rotationSpeed = 3; // the speed in which they rotate
    float neighbourDistance = 2; // the distance in which another boid becomes a neighbour (bigger the more are included)
    void Start()
    {
        speed = Random.Range(5, 15); //give the boid a random speed between 1 and 15
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, GlobalScriptNoGoal.boidStart) >= GlobalScriptNoGoal.areaLimit) //if the boid is outside or on the areas limit  
        {
            Vector3 direction = GlobalScriptNoGoal.boidStart - transform.position; //create vector that acts as the direction from where it is to the orignal start point
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime); //rotates the boid to point towards the new direction
            speed = Random.Range(5, 15); //gives it a new speed
        }
        else
        {
            if (Random.Range(0, 4) < 1) //limit to only being called 1/5 chances
            {
                ApplyRules(); //the rules in which a boid follows
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed); //move the boid at its speed
    }

    private void ApplyRules()
    {
        GameObject[] otherBoids;
        otherBoids = flockManager.allObjects; //fill the new game object array with the total boids array

        Vector3 center = Vector3.zero; //setting up the variable for the centre of the flock
        Vector3 avoid = Vector3.zero; //setting up the variable for the avoid distance

        float distance; //distance between current boid and another boid
        int groupSize = 0; //amount of boids within a group
        float averageSpeed = 0.5f; //average speed of the flock

        foreach (GameObject i in otherBoids) //loop through all the boids 
        {
            if (i != this.gameObject) //if the selected boid is not the current boid (every other boid apart from this one)
            {
                distance = Vector3.Distance(i.transform.position, this.transform.position); //distance is set to the distance between the two boids
                if (distance <= neighbourDistance) //if the boid is within neighbour distance
                {
                    center += i.transform.position; //add that boids position to the centre 
                    groupSize++; //increase the group size as their are now multiple in the group

                    if (distance < 1) //if distance is below 1
                    {
                        avoid = avoid + (this.transform.position - i.transform.position); //set the avoid to be the position minus the selected boids position
                    }
                    FlockScriptNoGoal anotherFlock = i.GetComponent<FlockScriptNoGoal>(); //get the other boids flock script
                    averageSpeed += anotherFlock.speed; //add their speed to the average speed
                }
            }
        }
        if (groupSize > 0) //if theres any boids in a group
        {
            center = center / groupSize - this.transform.position; //set the centre to the centre of the group (divide the current centre by the size of the group)
            speed = averageSpeed / groupSize; //set the speed to the average speed divided by the group size

            Vector3 direction = (center + avoid) - transform.position; //make a new direction so that it adjustes with the new centre and includes the avoid distance
            if (direction != GlobalScriptNoGoal.boidStart) // if the direction isnt facing the start point
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime); //rotate in the current direction

        }

    }
}
