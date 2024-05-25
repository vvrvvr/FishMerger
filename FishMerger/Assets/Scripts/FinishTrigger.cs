using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform center;
    [SerializeField] private ParticleSystem particle;
    private bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject == player && !isActive)
        {
            player.GetComponent<Player>().FinishGame();
            GameManager.Instance.WinGame();
            Instantiate(particle, center.position, Quaternion.identity);
            isActive = true;
        }
    }
}
