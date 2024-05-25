using UnityEngine;

public class Chaser : MonoBehaviour
{
    public float fishermanSpeed = 5f;
    public float fishermanmaxSpeed = 10f;
    public float accelerationTime = 1f;
    public float maxDistanceToFish = 5f;

    public Vector3 direction = Vector3.forward;
    public bool isChasing = true;
    public Transform fish;

    private float currentSpeed;
    private float targetSpeed;
    private float speedChangeRate;
    private bool wasBeyondMaxDistance;

    void Start()
    {
        currentSpeed = fishermanSpeed;
        wasBeyondMaxDistance = false;
    }

    void Update()
    {
        if (isChasing && fish != null)
        {
            float distanceToFish = Vector3.Distance(transform.position, fish.position);

            if (distanceToFish > maxDistanceToFish)
            {
                targetSpeed = fishermanmaxSpeed;
                speedChangeRate = (fishermanmaxSpeed - fishermanSpeed) / accelerationTime;

                if (!wasBeyondMaxDistance)
                {
                    Debug.Log("Distance to fish is greater than the maximum allowed.");
                    wasBeyondMaxDistance = true;
                }
            }
            else
            {
                targetSpeed = fishermanSpeed;
                speedChangeRate = (fishermanmaxSpeed - fishermanSpeed) / (accelerationTime / 2);

                if (wasBeyondMaxDistance)
                {
                    Debug.Log("Distance to fish is within the allowed range.");
                    wasBeyondMaxDistance = false;
                }
            }

            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, speedChangeRate * Time.deltaTime);
            Vector3 normalizedDirection = direction.normalized;
            Vector3 newPosition = transform.position + normalizedDirection * currentSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}