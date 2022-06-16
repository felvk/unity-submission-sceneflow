using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    public void LoadHighScore()
    {
        string filePath = GetFilePath();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            HighScore data = JsonUtility.FromJson<HighScore>(json);

            bestPlayerName = data.playerName;
            bestPlayerScore = data.playerScore;
        }
    }

    public void SaveHighScore()
    {
        HighScore highScore = new HighScore();
        highScore.playerName = bestPlayerName;
        highScore.playerScore = bestPlayerScore;

        string json = JsonUtility.ToJson(highScore);

        string filePath = GetFilePath();

        File.WriteAllText(filePath, json);
    }

    public string GetFilePath()
    {
        return Application.persistentDataPath + "/highscore.json";
    }

    [System.Serializable]
    class HighScore
    {
        public string playerName;
        public int playerScore;
    }
}
