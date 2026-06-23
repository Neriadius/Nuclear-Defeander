using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnerPath;
    [SerializeField] private List<GameObject> enemies;

    public void SpawnEnemy(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
        newEnemy.GetComponent<EnemyInteraction>().SetPath(spawnerPath);
    }

    public void SpawnRandomEnemy()
    {
        SpawnEnemy(enemies[Random.Range(0,enemies.Count - 1)]);
    }

    //test function
    void Start()
    {
        
    }

    /*IEnumerator EnemySpawnTest()
    {
        SpawnEnemy(enemies[0]);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(enemies[0]);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(enemies[0]);
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy(enemies[0]);
        yield return new WaitForSeconds(1.5f);
    }*/

    
}
