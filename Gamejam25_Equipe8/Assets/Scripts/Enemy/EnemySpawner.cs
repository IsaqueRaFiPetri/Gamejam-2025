using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuração do Spawner")]
    public GameObject enemyPrefab;   // Prefab do inimigo
    public int maxEnemies = 5;       // Número máximo de inimigos que podem nascer

    private int spawnedEnemies = 0;  // Contador de inimigos já spawnados
    private Collider2D areaCollider; // Collider usado para área de spawn

    private void Start()
    {
        // Pega o collider do objeto que tem o script
        areaCollider = GetComponent<Collider2D>();

        if (areaCollider == null || !areaCollider.isTrigger)
        {
            Debug.LogError("Este objeto precisa ter um Collider2D marcado como 'Is Trigger'.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawnedEnemies < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos = GetRandomPointInArea();
        Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        spawnedEnemies++;
    }

    Vector2 GetRandomPointInArea()
    {
        Bounds bounds = areaCollider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }
}
