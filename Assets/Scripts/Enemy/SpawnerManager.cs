using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] private float cooldownTime = 5f;
    [SerializeField] private float timeRange = 2f; 

    void Awake()
    {
        enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
    }

    void Start()
    {
        StartCoroutine(Cooldown());
    }

    // Update is called once per frame
    IEnumerator Cooldown()
    {
        int spawnerIndex = Random.Range(0,enemySpawners.Length - 1);
        yield return new WaitForSeconds(Random.Range(cooldownTime - timeRange,cooldownTime + timeRange));
        enemySpawners[spawnerIndex].SpawnRandomEnemy();
        StartCoroutine(Cooldown());
    }
    
}
