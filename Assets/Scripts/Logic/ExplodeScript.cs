using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplodeScript : MonoBehaviour
{

    public float explosionDelay = 3f;
    public AudioSource click;

    public GameObject player;
    public AudioSource explosion;
    void Start()
    {
        
    }

    public void ExplodeAES()
    {
       
        Debug.Log("Explode");
        StartCoroutine(CountdownToExecution());

    }

    IEnumerator CountdownToExecution()
    {
        click.Play();
        explosion.Play();
        yield return new WaitForSeconds(explosionDelay);
        SceneManager.LoadScene(2);
    }

}
