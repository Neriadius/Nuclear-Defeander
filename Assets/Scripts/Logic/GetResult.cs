using System.Collections;
using TMPro;
using UnityEngine;

public class GetResult : MonoBehaviour
{
    public scoreObject scoreManager;
    public TMP_Text resultText;
    public TMP_Text valueText;
    public TMP_Text totalText;
    public float typingSpeed;
    private void Awake()
    {
        scoreManager = FindFirstObjectByType<scoreObject>();

        StartCoroutine(WriteText());
    }
    private void Update()
    {
        totalText.text = PlayerPrefs.GetInt("score").ToString();
    }

    IEnumerator WriteText()
    {
        resultText.text = "";
        valueText.text = "";
        resultText.maxVisibleCharacters = 0;
        foreach (var log in scoreManager.logs)
        {
            int score = Mathf.RoundToInt(log.score);
            string line = $"{log.massage}:\n";
            int start = resultText.text.Length;
            resultText.text += line;
            while (resultText.maxVisibleCharacters < start + line.Length)
            {
                resultText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
            valueText.text += score + "\n";
        }
    }
}
