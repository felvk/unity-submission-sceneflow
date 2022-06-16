using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance;

    public string playerName = "";
    public string bestPlayerName = "";

    public int playerScore = 0;
    public int bestPlayerScore = 0;

    // Start is called before the first frame update
    void Awake()
    {
        LoadHighScore();

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class HighScore
    {
        public string playerName;
        public string playerScore;
    }

    public void LoadHighScore()
    {
        bestPlayerName = "";
        bestPlayerScore = 0;
    }

    public void SaveHighScore()
    {

    }
}
