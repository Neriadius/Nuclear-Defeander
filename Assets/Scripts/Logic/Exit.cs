using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
    public void BuyMag()
    {
        if(PlayerPrefs.GetInt("score") >= 50)
        {
            PlayerPrefs.SetInt("SpawnerMagCount", PlayerPrefs.GetInt("SpawnerMagCount") + 1);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") -50);
        }
        
    }
}
