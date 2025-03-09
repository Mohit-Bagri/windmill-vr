using UnityEngine;

public class Windmill : MonoBehaviour
{
    public Transform rotator; // Assign the rotating part of the windmill
    public float maxSpeed = 200f; // Maximum rotation speed
    public float accelerationTime = 2f; // Time to reach max speed

    private float currentSpeed = 0f;
    private bool isExhaling = false;

    void Update()
    {
        if (isExhaling)
        {
            // Gradually increase speed
            if (currentSpeed < maxSpeed)
                currentSpeed += (maxSpeed / accelerationTime) * Time.deltaTime;
        }
        else
        {
            // Gradually slow down after exhale
            if (currentSpeed > 0)
                currentSpeed -= (maxSpeed / accelerationTime) * Time.deltaTime;
        }

        // Apply rotation
        rotator.Rotate(Vector3.forward, currentSpeed * Time.deltaTime);
    }

    public void StartWindmill()
    {
        isExhaling = true;
    }

    public void StopWindmill()
    {
        isExhaling = false;
    }
}
