using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public AudioSource click;

    public void ReloadMainScene()
    {
        click.Play();
        SceneManager.LoadScene(1);
    }
    public void Reset()
    {
        click.Play();
        PlayerPrefs.DeleteAll();
    }

}
