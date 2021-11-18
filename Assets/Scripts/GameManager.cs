using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Datastructures

    [Serializable]
    public struct Highscore
    {
        public string name;
        public int score;
    }

    [Serializable]
    public class SaveData
    {
        public Highscore[] highscores;
    }
    #endregion


    #region Singleton

    public static GameManager ShareInstance;

    private void Awake()
    {
        if(ShareInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            ShareInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [HideInInspector] public Highscore[] highscores = new Highscore[8];
    [HideInInspector] public Highscore player;

    public static void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public bool UpdateHighscores()
    {
        var isNewHighscore = false;

        for (var i = 0; i < highscores.Length; i++)
        {
            if (player.score <= highscores[i].score) continue;

            for (var j = highscores.Length - 2; j >= i; j--)
            {
                highscores[j + 1].name = highscores[j].name;
                highscores[j + 1].score = highscores[j].score;
            }

            highscores[i].name = player.name;
            highscores[i].score = player.score;
            isNewHighscore = true;
            break;
        }

        return isNewHighscore;
    }

    public void LoadHighscores()
    {
        var filepath = Application.persistentDataPath + "/highscores.json";

        if (!File.Exists(filepath)) return;

        var save = JsonUtility.FromJson<SaveData>(File.ReadAllText(filepath));

        if (save != null)
        {
            highscores = save.highscores;
        }
    }

    public void SaveHighscores()
    {
        var save = new SaveData()
        {
            highscores = this.highscores
        };

        File.WriteAllText(Application.persistentDataPath + "/highscores.json", JsonUtility.ToJson(save));
    }
}
