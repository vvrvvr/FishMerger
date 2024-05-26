
using UnityEngine;

public class MurilRun : MonoBehaviour
{
    public float currentSpeed = 1f;
    private Vector3 direction = Vector3.right;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + direction * currentSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
