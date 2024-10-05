using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duration of the shake
    public float shakeMagnitude = 0.3f; // Magnitude of the shake (how strong the shake is)
    public float dampingSpeed = 1.0f; // How fast the shake dampens (fades away)

    private Vector3 initialPosition;
    private float currentShakeDuration = 0f;

    void OnEnable()
    {
        // Store the initial position of the object
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Check if there's still shake time left
        if (currentShakeDuration > 0)
        {
            // Apply the shake by adding random offset to the object's position
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            // Reduce the shake duration over time
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
            Debug.Log("Shake called");
        }
        else
        {
            // Once the shake is done, reset the object back to its original position
            currentShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    // Call this method to start the shake effect
    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        currentShakeDuration = shakeDuration;
    }
}
