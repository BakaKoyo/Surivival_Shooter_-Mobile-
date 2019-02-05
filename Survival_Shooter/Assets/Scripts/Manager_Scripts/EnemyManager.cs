using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    Enemy enemy_Prefab;

    [SerializeField]
    float spawnEnemyEveryMin = 4f;

    [SerializeField]
    float spawnEnemyEveryMax = 10f;

    [SerializeField]
    float spawnDistanceFromPlayer = 20f;

    [SerializeField]
    float min_spawnDistanceFromPlayer = 5f;

    public IEnumerator StartEnemySpawning()
    {
        while (GameManager.Instance.IsGameOver == false)
        {
            yield return new WaitForSeconds(RandonSpawnTime());
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        /* If the enemy_prefab is not null
              Instantiate the Enemy prefab */
        if(enemy_Prefab != null)
        {
            Enemy enemy = Instantiate(enemy_Prefab, SpawnPosition(), enemy_Prefab.transform.rotation);
        }
    }

    /* 
       Random spawn time
          - generates a random number from the stated min to a max 
    */
    float RandonSpawnTime() {
        return Random.Range(spawnEnemyEveryMin, spawnEnemyEveryMax);
    }

    Vector3 SpawnPosition()
    {
        Vector2 randomPos = new Vector2(Random.Range(-spawnDistanceFromPlayer, spawnDistanceFromPlayer),
                                        Random.Range(-spawnDistanceFromPlayer, spawnDistanceFromPlayer));

        if (Vector2.Distance(GameManager.Instance.Player.transform.position, randomPos) <= min_spawnDistanceFromPlayer)
        {
            randomPos.x = min_spawnDistanceFromPlayer;
        }

        Vector3 spawnPos = new Vector3(randomPos.x - GameManager.Instance.Player.transform.position.x,
                                       GameManager.Instance.Player.transform.position.y,
                                       randomPos.y - GameManager.Instance.Player.transform.position.z);

        return spawnPos;
       

    }

}
