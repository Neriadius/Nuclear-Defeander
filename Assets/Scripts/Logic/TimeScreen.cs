using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeScreen : MonoBehaviour
{
    public TMP_Text timeText;
    public float delay = 60f;
    public int time = 0;

    void Awake()
    {
        StartCoroutine(TimerScreen());
    }

    IEnumerator TimerScreen()
    {
        timeText.text = "0" + time.ToString() + ":00";
        yield return new WaitForSeconds(delay);
        time++;

        if (time == 8)
        {
            SceneManager.LoadScene(2);
        }

        StartCoroutine(TimerScreen());
    }
}
