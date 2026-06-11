using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
