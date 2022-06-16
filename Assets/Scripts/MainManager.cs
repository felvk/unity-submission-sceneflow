using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    public GameObject SuccessText;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private string m_PlayerName = "";

    private int m_bricksCount = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        if (Storage.Instance)
        {
            m_PlayerName = Storage.Instance.playerName;
        }

        RenderBestScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                m_bricksCount++;
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Your Score : {m_Points}";
        m_bricksCount--;

        if (m_bricksCount <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (Storage.Instance && m_Points > Storage.Instance.bestPlayerScore)
        {
            SetBestScore(m_Points, m_PlayerName);
        }

        RenderBestScore();

        m_GameOver = true;

        if (m_bricksCount <= 0)
        {
            SuccessText.SetActive(true);
        }
        else
        {
            GameOverText.SetActive(true);
        }
    }

    public void SetBestScore(int score, string name)
    {
        if (Storage.Instance)
        {
            Storage.Instance.bestPlayerName = name;
            Storage.Instance.bestPlayerScore = score;

            Storage.Instance.SaveHighScore();
        }
    }

    public void RenderBestScore()
    {
        string name = "";
        int score = 0;

        if (Storage.Instance)
        {
            name = Storage.Instance.bestPlayerName;
            score = Storage.Instance.bestPlayerScore;
        }

        if (name != "" && score > 0)
        {
            BestScoreText.text = $"Best Score: {name} - {score}";
        } 
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
