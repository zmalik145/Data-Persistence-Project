using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    public Text bestScoreText;
    

    private void Start()
    {
        ShowBestScorer();
    }
    public void ShowBestScorer()
    {
        bestScoreText.text = $"Best Score : {GameController.Instance.highScorePlayer} : {GameController.Instance.bestScore} " ;
    }

    public void BackToMenu()
    {
        GameController.Instance.SaveHighScorerData();
        SceneManager.LoadScene(0);
    }

}
