using UnityEngine;
using System.Collections;

public class AmbientScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] ambientSounds;
    public float minDelay = 1f;
    public float maxDelay = 5f;

    void Start()
    {
        StartCoroutine(RandomSound());
    }

    IEnumerator RandomSound()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        
        audioSource.clip = ambientSounds[Random.Range(0, ambientSounds.Length)];
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        StartCoroutine(RandomSound());
    }
}
