using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public List<EnemySO> enemies;

    public int initialPoolSize = 10;
    readonly Dictionary<EnemySO, Queue<GameObject>> pools = new();

    void Awake()
    {
        foreach (var enemy in enemies)
        {
            pools[enemy] = new Queue<GameObject>();
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(enemy.enemyPrefab);
                obj.SetActive(false);
                pools[enemy].Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(EnemySO enemy)
    {
        if (!pools.ContainsKey(enemy) || pools[enemy].Count == 0)
        {
            GameObject newObj = Instantiate(enemy.enemyPrefab);
            newObj.SetActive(false);
            if (newObj.GetComponent<Enemy>() == null) newObj.AddComponent<Enemy>();
            pools[enemy].Enqueue(newObj);
        }
        GameObject obj = pools[enemy].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        // pools[obj].Enqueue(obj);
        foreach (var key in pools.Keys)
        {
            if (obj.name.Contains(key.enemyPrefab.name))
            {
                pools[key].Enqueue(obj);
                return;
            }
        }
    }
}
