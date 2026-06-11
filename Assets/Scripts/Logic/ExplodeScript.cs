using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplodeScript : MonoBehaviour
{

    public float explosionDelay = 3f;

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
        yield return new WaitForSeconds(explosionDelay);
        SceneManager.LoadScene(1);
    }

}
