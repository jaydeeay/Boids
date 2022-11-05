using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField]
    public int SwarmIndex;
    [SerializeField]
    public float NoClumpingRadius = 5f;
    [SerializeField]
    public float LocalAreaRadius = 5f;
    [SerializeField]
    public float Speed = 10f;
    [SerializeField]
    public float SteeringSpeed = 100f;

    public void SimulateMovement(List<Boid> other, float time)
    {
        var steering = Vector3.zero;
        var separationDirection = Vector3.zero;
        var separationCount = 0;
        var alignmentDirection = Vector3.zero;
        var alignmentCount = 0;
        var cohesionDirection = Vector3.zero;
        var cohesionCount = 0;


        foreach (Boid boid in other)
        {
            if (boid == this)
                continue;

            var distance = Vector3.Distance(boid.transform.position, this.transform.position);

            if (distance < NoClumpingRadius)
            {
                separationDirection += boid.transform.position - transform.position;
                separationCount++;
            }

            if (distance < LocalAreaRadius && boid.SwarmIndex == this.SwarmIndex)
            {
                alignmentDirection += boid.transform.forward;
                alignmentCount++;

                cohesionDirection += boid.transform.position - transform.position;
                cohesionCount++;


            }
        }

        if (separationCount > 0)
            separationDirection /= separationCount;

        separationDirection = -separationDirection;

        if (alignmentCount > 0)
            alignmentDirection /= alignmentCount;

        if (cohesionCount > 0)
            cohesionDirection /= cohesionCount;

        cohesionDirection -= transform.position;

        steering += separationDirection.normalized;
        steering += alignmentDirection.normalized;
        steering += cohesionDirection.normalized;



        if (steering != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(steering), SteeringSpeed * time);

        transform.position += transform.TransformDirection(new Vector3(0, 0, Speed)) * time;

    }

}