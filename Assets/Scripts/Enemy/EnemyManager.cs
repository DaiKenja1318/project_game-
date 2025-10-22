using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [System.Serializable]
    public class EnemyPool
    {
        public string enemyType;
        public GameObject prefab;
        public int initialCount = 10;
    }

    [Header("Enemy Pools")]
    public List<EnemyPool> enemyPools;

    [Header("Camera Settings")]
    public Camera mainCamera;
    [Tooltip("Khoảng mở rộng vùng camera (để spawn enemy trước khi nó xuất hiện).")]
    public float spawnBuffer = 3f;

    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        InitializePools();

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void InitializePools()
    {
        foreach (var pool in enemyPools)
        {
            Queue<GameObject> enemyQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.initialCount; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                enemyQueue.Enqueue(obj);
            }
            poolDictionary.Add(pool.enemyType, enemyQueue);
        }
    }

    private void Update()
    {
        UpdateActiveEnemies();
    }

    private void UpdateActiveEnemies()
    {
        Vector2 min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        min.x -= spawnBuffer;
        max.x += spawnBuffer;
        min.y -= spawnBuffer;
        max.y += spawnBuffer;

        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = activeEnemies[i];
            if (enemy == null) continue;

            Vector2 pos = enemy.transform.position;
            if (pos.x < min.x || pos.x > max.x || pos.y < min.y || pos.y > max.y)
            {
                // Enemy ra khỏi vùng camera → despawn
                EnemyBase baseScript = enemy.GetComponent<EnemyBase>();
                if (baseScript != null)
                    baseScript.OnDespawn();
                else
                    DespawnEnemy("Unknown", enemy);

                activeEnemies.RemoveAt(i);
            }
        }
    }

    public GameObject SpawnEnemy(string type, Vector2 position)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Enemy type '{type}' not found in pool!");
            return null;
        }

        Vector2 min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        min.x -= spawnBuffer;
        max.x += spawnBuffer;
        min.y -= spawnBuffer;
        max.y += spawnBuffer;

        // Chỉ spawn nếu nằm trong vùng camera (hoặc buffer)
        if (position.x < min.x || position.x > max.x || position.y < min.y || position.y > max.y)
            return null;

        GameObject enemyToSpawn = poolDictionary[type].Count > 0
            ? poolDictionary[type].Dequeue()
            : Instantiate(enemyPools.Find(p => p.enemyType == type).prefab);

        enemyToSpawn.transform.position = position;
        enemyToSpawn.SetActive(true);
        activeEnemies.Add(enemyToSpawn);
        return enemyToSpawn;
    }

    public void DespawnEnemy(string type, GameObject enemy)
    {
        if (enemy == null) return;

        enemy.SetActive(false);
        if (poolDictionary.ContainsKey(type))
            poolDictionary[type].Enqueue(enemy);
        else
            Destroy(enemy);
    }
}
