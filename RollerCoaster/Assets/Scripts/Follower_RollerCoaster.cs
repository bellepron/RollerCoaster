using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower_RollerCoaster : MonoBehaviour
{
    PathCreator pathCreator;
    public float speed = 0;
    public float distanceTravelled;

    void Start()
    {
        pathCreator = FindObjectOfType<PathCreator>();
    }

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}
