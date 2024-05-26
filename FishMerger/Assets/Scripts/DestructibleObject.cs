using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject vfxPrefab; // Префаб VFX, который будет запущен при разрушении
    public string playerTag = "Player";
    public string bonesTag = "ForExplosion";	// Тег для объекта игрока 
    public Vector3 vfxScale = Vector3.one; // Масштаб VFX
    public float destructionDelay = 0.00f; // Задержка перед уничтожением объекта (в секундах)

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.name); // Отладка коллизии
        // Проверяем, что объект, с которым произошла коллизия, имеет тег "Player"
        if (collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(bonesTag))
        {
            //Debug.Log("Collision with Player detected.");
            // Запускаем VFX
            GameObject vfxInstance = Instantiate(vfxPrefab, transform.position, transform.rotation);
            vfxInstance.transform.localScale = vfxScale;

            // Уничтожаем разрушаемый объект с задержкой
            Destroy(gameObject, destructionDelay);
        }
    }
}

