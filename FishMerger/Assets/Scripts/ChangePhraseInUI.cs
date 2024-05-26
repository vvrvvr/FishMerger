using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangePhraseInUI : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public bool isEnd = false;
    public string[] Phrases = new string[3];

    public int phraseIndex = 0;

    private void Start()
    {
        Text.text = Phrases[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePhrase();
        }
    }

    private void ChangePhrase()
    {
        phraseIndex++;
        if (phraseIndex >= Phrases.Length)
        {
            LoadLevel();
        }
        else
        {
            Text.text = Phrases[phraseIndex];
        }
    }

    public void LoadLevel()
    {
        if (!isEnd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
        Debug.Log("load level");
    }
}
