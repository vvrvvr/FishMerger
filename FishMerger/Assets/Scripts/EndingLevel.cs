using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingLevel : MonoBehaviour
{

    public GameObject UIEnding; // UI ������, ������� ����� ��������

    void OnTriggerEnter(Collider other)
    {
        
        if ( other.CompareTag("Player"))
        {
            
            UIEnding.SetActive(true);
        }
    }

}
