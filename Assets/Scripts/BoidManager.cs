using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{

    [SerializeField]private Boid boidPrefab;

    [SerializeField]private int spawnBoids = 50;

    [SerializeField] BoidConfig config;

    private List<Boid> _boids;

    private void Start()
    {
        _boids = new List<Boid>();

        for (int i = 0; i < spawnBoids; i++)
        {
            SpawnBoid(boidPrefab.gameObject);
        }
    }

    private void Update()
    {
        foreach (Boid boid in _boids)
        {
            boid.SimulateMovement(_boids, Time.deltaTime);

        }
    }

    private void SpawnBoid(GameObject prefab)
    {
        var boidInstance = Instantiate(prefab);
        boidInstance.transform.localPosition += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        var boidController = boidInstance.GetComponent<Boid>();
        boidController.config = config;


        _boids.Add(boidController);
    }


}
