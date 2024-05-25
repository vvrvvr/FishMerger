using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private bool isground;
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            isground = true;
            //Debug.Log("ground");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            isground = false;
        }
    }

    public bool CheckComponentGround()
    {
        if (isground)
            return true;
        else
            return false;
    }
}
