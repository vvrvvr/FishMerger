using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltip;
    public string phrase;
    public TextMeshProUGUI phraseText;

    private void Start()
    {
        tooltip.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if ( other.CompareTag("Player"))
        {
            phraseText.text = phrase;
            tooltip.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.CompareTag("Player"))
        {
            tooltip.SetActive(false);
        }
    }
}
