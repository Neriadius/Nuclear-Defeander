using TMPro;
using UnityEngine;

public class GetResult : MonoBehaviour
{
    public scoreObject scoreManager;
    public TMP_Text resultText;
    public TMP_Text valueText;

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



    void DisplayResults()
    {
        scoreManager.TotalScore();
        resultText.text = "Total Score: " + scoreManager.total.ToString();
    }
}
