using TMPro;
using UnityEngine;

public class GetResult : MonoBehaviour
{
    public scoreObject scoreManager;
    public TMP_Text resultText;
    public TMP_Text valueText;
    public TMP_Text totalText;
    private void Awake()
    {
        scoreManager = FindFirstObjectByType<scoreObject>();
        foreach (var log in scoreManager.logs)
        {
            int score = Mathf.RoundToInt(log.score);
            resultText.text += log.massage + ": " + "\n";
            valueText.text += score.ToString() + "\n";
        }
    }
    private void Update()
    {
        totalText.text = PlayerPrefs.GetInt("score").ToString();
    }


    void DisplayResults()
    {
        scoreManager.TotalScore();
        resultText.text = "Total Score: " + scoreManager.total.ToString();
    }
}
