using System.Collections;
using System;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] private float cooldownTime = 5f;
    [SerializeField] private float timeRange = 2f;
    private int enemyPoolCount;
    void Awake()
    {
        enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        int level = PlayerPrefs.GetInt("lvl");
        Debug.Log("lvl is " + level);
        enemyPoolCount = (int)Math.Floor(12*Math.Log(2*level) + 1);
        Debug.Log("enemy count is " + enemyPoolCount);
    }

    void Start()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        int spawnerIndex = UnityEngine.Random.Range(0,enemySpawners.Length);
        yield return new WaitForSeconds(UnityEngine.Random.Range(cooldownTime - timeRange,cooldownTime + timeRange));
        if(enemyPoolCount > 0){
            enemySpawners[spawnerIndex].SpawnRandomEnemy();
            enemyPoolCount--;}
        StartCoroutine(Cooldown());
    }
    
}
