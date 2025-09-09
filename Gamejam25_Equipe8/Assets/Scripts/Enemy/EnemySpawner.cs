using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private float EnemySpawnCoolDown = 1.5f;
    [SerializeField] private int maxEnemies = 10;

    private Collider2D areaCollider;
    private int enemyCount = 0;
    bool isSpawning;
    private void Awake()
    {
        areaCollider = GetComponent<Collider2D>();

        if (areaCollider == null)
        {
            Debug.LogError("Nenhum Collider2D encontrado no spawner!");
        }
    }

    private void Start()
    {
        
    }

    private IEnumerator SpawnEnemy(float interval, GameObject Enemy)
    {
        while (enemyCount < maxEnemies)
        {
            yield return new WaitForSeconds(interval);

            Vector2 spawnPos = GetRandomPointInCollider(areaCollider);

            Instantiate(Enemy, spawnPos, Quaternion.identity);
            enemyCount++;
        }
    }

    private Vector2 GetRandomPointInCollider(Collider2D col)
    {
        Bounds bounds = col.bounds;
        Vector2 point;
        do
        {
            point = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
        }
        while (!col.OverlapPoint(point)); 
        return point;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision);
        if (isSpawning)
            return;

        StartCoroutine(SpawnEnemy(EnemySpawnCoolDown, EnemyPrefab));
        isSpawning = true;
    }
}
