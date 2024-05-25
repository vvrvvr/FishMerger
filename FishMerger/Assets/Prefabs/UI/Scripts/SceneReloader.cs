using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneReloader : MonoBehaviour
{
    public Button restartButton;

    void Start()
    {
        // Add a listener to the button to call the ReloadScene method when clicked
        restartButton.onClick.AddListener(ReloadScene);
    }

    void ReloadScene()
    {
        // Get the current active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
