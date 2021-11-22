using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public string playerName;
    public string highScorePlayer;

    public int bestScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    [System.Serializable]
    class SaveData
    {
        public string highScorePlayer;
        public int bestScore;
    }

    public void SaveHighScorerData()
    {
        SaveData data = new SaveData();
        data.highScorePlayer = highScorePlayer;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScorerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        highScorePlayer = data.highScorePlayer;
        bestScore = data.bestScore;
        
    }

}
