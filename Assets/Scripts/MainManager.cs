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

    public Text textBestScore;
    public Text textPlayerScore;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool isGameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        SetBestScore();

        GameManager.ShareInstance.player.score = 0;
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
        else if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
#if UNITY_EDITOR
                if (GameManager.ShareInstance == null) return;
#endif
                GameManager.StartGame();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
#if UNITY_EDITOR
                if (GameManager.ShareInstance == null) return;
#endif
                GameManager.OpenMenu();
            }
        }
    }

    void AddPoint(int point)
    {
        GameManager.ShareInstance.player.score += point;
        textPlayerScore.text = $"Score : {GameManager.ShareInstance.player.score}";
    }

    public void GameOver()
    {
        if (GameManager.ShareInstance.UpdateHighscores())
        {
            GameManager.ShareInstance.SaveHighscores();
            SetBestScore();
        }

        isGameOver = true;
        GameOverText.SetActive(true);
    }

    private void SetBestScore()
    {
        var highscore = GameManager.ShareInstance.highscores[0];
        textBestScore.text = "Best Highscore: " + highscore.name + " with " + highscore.score + " points.";
    }
}
