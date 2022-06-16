using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public InputField playerNameInput;
    public Text bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        RenderBestScore();
    }

    public void StartGame()
    {
        setPlayer();

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void setPlayer()
    {
        string playerName = playerNameInput.text != "" ? playerNameInput.text : "Nobody";

        if (Storage.Instance)
        {
            Storage.Instance.playerName = playerName;
        }
    }

    public void RenderBestScore()
    {
        if (Storage.Instance && Storage.Instance.bestPlayerName != "" && Storage.Instance.bestPlayerScore > 0)
        {
            bestScoreText.text = $"Best Score \n{Storage.Instance.bestPlayerName}: {Storage.Instance.bestPlayerScore}";
        }
    }
}
