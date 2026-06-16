using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class scoreObject : MonoBehaviour
{
    public List<log> logs = new List<log>();
    public int total;
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
        foreach (var log in logs) { total = log.score; }
    }
}
