using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public AudioClip[] zombieGrowls;
    public AudioClip[] zombieDeaths;
    public AudioSource AudioSource;

    private EnemyInteraction EnemyInteraction;

    private void Awake()
    {
        EnemyInteraction = GetComponent<EnemyInteraction>();
        zombieApperience();
    }

    private void Update()
    {
        if (EnemyInteraction.currentHealth == 0)
        {
            ZombieDaethSound();
        }
    }

    private void zombieApperience()
    {
        AudioSource.clip = zombieGrowls[Random.Range(0, zombieGrowls.Length - 1)];
        AudioSource.Play();
    }

    private void ZombieDaethSound()
    {
        AudioSource.clip = zombieDeaths[Random.Range(0, zombieDeaths.Length - 1)];
    }

}
