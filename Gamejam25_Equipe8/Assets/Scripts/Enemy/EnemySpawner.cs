using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuração dos inimigos")]
    [SerializeField] private GameObject[] enemyPrefabs; // array com 3 tipos diferentes
    [SerializeField] private float enemySpawnCoolDown = 1.5f;
    [SerializeField] private int maxEnemies = 10;

    private Collider2D areaCollider;
    private int enemyCount = 0;
    private bool isSpawning;

    private void Awake()
    {
        areaCollider = GetComponent<Collider2D>();
    }

    private IEnumerator SpawnEnemies(float interval)
    {
        while (enemyCount < maxEnemies)
        {
            yield return new WaitForSeconds(interval);

            Vector2 spawnPos = GetRandomPointInCollider(areaCollider);

            // Escolhe aleatoriamente um inimigo
            int index = Random.Range(0, enemyPrefabs.Length);
            GameObject prefab = enemyPrefabs[index];

            Instantiate(prefab, spawnPos, Quaternion.identity);
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
        if (isSpawning)
            return;

        StartCoroutine(SpawnEnemies(enemySpawnCoolDown));
        isSpawning = true;
    }
}
