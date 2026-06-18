using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Content.Interaction;


public class scoreObject : MonoBehaviour
{
    public List<log> logs = new List<log>();
    public float total;
    public int doorCount;

    private static GameObject Instance;

    //public bool onlyOneTime = true;

    private int scoreSave;
    private int sceneIndex;

    private void Awake()
    {        
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = gameObject;
        DontDestroyOnLoad(this.gameObject);

    }

    public void AddScore(log log)
    {
        int i = 0;
        for (i=0; i < logs.Count; i++) {
            if (logs[i].massage == log.massage)
            {
                logs[i].score += log.score;
                break;
            }
        }
        if (i == logs.Count) {
            logs.Add(log);
        }
        
    }

    public void TotalScore()
    {
        total = 0;
        foreach (var log in logs) { total += log.score; }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneIndex)
        {
            TotalScore();
            //onlyOneTime = false;
            scoreSave = PlayerPrefs.GetInt("score");
            scoreSave += (int)total;
            PlayerPrefs.SetInt("score", scoreSave);
            Debug.Log(PlayerPrefs.GetInt("score"));
            logs.Clear();
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        DoorCounter(); 
    }

    void DoorCounter()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            doorCount = 0;
        }
        else 
        {
            AddScore(new log("Doors", -0.5f * doorCount * Time.deltaTime));
        }
    }
}
