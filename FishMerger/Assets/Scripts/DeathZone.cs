using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public float cooldown = 1f; // Время кулдауна в секундах
    private bool isOnCooldown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOnCooldown && other.CompareTag("Player"))
        {
            Debug.Log("сработало");
            PlayerController.Instance.RespawnToNearPoint();
            StartCoroutine(CooldownRoutine());
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}