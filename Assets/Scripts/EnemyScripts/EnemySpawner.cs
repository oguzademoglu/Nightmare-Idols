using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] List<EnemySO> enemies = new();
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float spawnRadius = 10f;
    [SerializeField] float despawnDistance = 30f;

    Transform player;

    void Start()
    {
        player = FindAnyObjectByType<Player>().transform;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        InvokeRepeating(nameof(CheckAndDestroyEnemies), 5f, 5f);
    }

    void SpawnEnemy()
    {
        if (enemies.Count == 0) return;
        EnemySO enemyData = enemies[Random.Range(0, enemies.Count)];
        GameObject enemyObject = enemyPool.GetObject(enemyData);
        if (enemyObject == null) return;

        enemyObject.transform.position = GetOffSpawnScreenPosition();
        enemyObject.SetActive(true);

        Enemy enemyComponent = enemyObject.GetComponent<Enemy>();
        enemyComponent.Initiliaze(enemyData, player, enemyPool);

    }

    Vector2 GetOffSpawnScreenPosition()
    {
        Camera cam = Camera.main;
        if (cam == null) return Vector2.zero;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Vector2 screenCenter = cam.transform.position;
        Vector2 screenCenter = player.position;
        int side = Random.Range(0, 4); // 0 = üst, 1 = alt, 2 = sol, 3 = sağ
        Vector2 spawnPos = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPos = new Vector2(Random.Range(screenCenter.x - camWidth / 2, screenCenter.x + camWidth / 2), screenCenter.y + camHeight / 2 + spawnRadius);
                break;
            case 1:
                spawnPos = new Vector2(Random.Range(screenCenter.x - camWidth / 2, screenCenter.x + camWidth / 2), screenCenter.y - camHeight / 2 - spawnRadius);
                break;
            case 2:
                spawnPos = new Vector2(screenCenter.x - camWidth / 2 - spawnRadius, Random.Range(screenCenter.y - camHeight / 2, screenCenter.y + camHeight / 2));
                break;
            case 3:
                spawnPos = new Vector2(screenCenter.x + camWidth / 2 + spawnRadius, Random.Range(screenCenter.y - camHeight / 2, screenCenter.y + camHeight / 2));
                break;
        }
        return spawnPos;
    }


    void CheckAndDestroyEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector2.Distance(enemy.transform.position, player.position) > despawnDistance)
            {
                enemyPool.ReturnObject(enemy);
            }
        }
    }
}
