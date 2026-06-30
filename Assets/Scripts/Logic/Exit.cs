using UnityEngine;

public class Exit : MonoBehaviour
{
    public AudioSource click;

    public void ExitGame()
    {
        click.Play();
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
    public void BuyMag()
    {
        click.Play();
        if(PlayerPrefs.GetInt("score") >= 50)
        {
            PlayerPrefs.SetInt("SpawnerMagCount", PlayerPrefs.GetInt("SpawnerMagCount") + 1);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") -50);
        }
        
    }
}
