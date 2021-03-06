using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float speed;
    float rotationSpeed;

    private void Start()
    {
        rotationSpeed -= 1f / 86400f * speed;       //set rotationSpeed
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0);     //continously rotate the game object around the y axis at rotationSpeed
    }
}
