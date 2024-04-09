using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveUpDown : MonoBehaviour
{
    public float amplitude = 0.05f; // Height of the oscillation
    public float frequency = 0.25f; // Speed of the oscillation

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Store the starting position
    }

    void Update()
    {
        Vector3 newPos = startPos;
        newPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude; // Calculate the new Y position
        transform.position = newPos; // Apply the new position
    }
}

