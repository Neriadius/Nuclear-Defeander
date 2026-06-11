using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplodeScript : MonoBehaviour
{

    public float explosionDelay = 3f;
    public AudioSource click;
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
        yield return new WaitForSeconds(explosionDelay);
        SceneManager.LoadScene(1);
    }

}
