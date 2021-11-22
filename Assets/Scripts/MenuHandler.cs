using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;


public class MenuHandler : MonoBehaviour
{

    public InputField playerNameField;
    public TextMeshProUGUI bestScoreText;

    private void Start()
    {
        GameController.Instance.LoadHighScorerData();
        bestScoreText.text = $"Best Score : {GameController.Instance.highScorePlayer} : {GameController.Instance.bestScore}";
    }
    public void ReadInputFromField()
    {
        GameController.Instance.playerName = playerNameField.text;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameController.Instance.SaveHighScorerData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
