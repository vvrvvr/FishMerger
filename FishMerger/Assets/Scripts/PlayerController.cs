using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float impulseForceMin = 1f;
    public float impulseForceMax = 10f;
    public float forceTime = 4f;

    private Vector3 impulseDirection = new Vector3(1, 1, 0);
    private bool isPressing = false;
    private float pressStartTime;
    public Player player;
    public HealthBar forceSlider;

    private float currentForce = 0f;

    public void Start()
    {
        forceSlider.SetValues(impulseForceMin, impulseForceMax);
    }

    void Update()
    {
        bool isGrounded = player.isGrounded;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPressing = true;
            pressStartTime = Time.time;
        }

        if (isPressing && Input.GetKey(KeyCode.Space))
        {
            float heldTime = Time.time - pressStartTime;
            float t = Mathf.Clamp01(heldTime / forceTime);
            currentForce = Mathf.Lerp(impulseForceMin, impulseForceMax, t);
            forceSlider.SetForce(currentForce);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isPressing = false;
            if(isGrounded)
                player.Push(impulseDirection * currentForce);
            currentForce = impulseForceMin;
            forceSlider.SetForce(currentForce);
        }
    }
}