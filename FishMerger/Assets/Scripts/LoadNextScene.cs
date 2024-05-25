using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour
{
    public Button nextSceneButton;

    void Start()
    {
        // Получаем компонент Button на этом объекте
        // Добавляем слушатель нажатия на кнопку
        nextSceneButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Проверяем, активна ли кнопка
        if (nextSceneButton.interactable)
        {
            // Загрузка следующей сцены
            LoadNextSceneByIndex();
        }
	}

    void LoadNextSceneByIndex()
    {
        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Загружаем следующую сцену по индексу
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

