using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject foodPrefab; // Prefab del alimento
    public int maxFood = 10; // Cantidad máxima de alimentos en el mapa
    public float spawnInterval = 5f; // Intervalo de tiempo entre spawns

    [Header("Área de Spawn")]
    public Collider2D spawnArea; // Área donde se generarán los alimentos

    private int currentFoodCount = 0;

    void Start()
    {
        // Generar alimentos iniciales
        for (int i = 0; i < maxFood; i++)
        {
            SpawnFood();
        }

        // Iniciar el spawn continuo de alimentos
        StartCoroutine(SpawnFoodOverTime());
    }

    private System.Collections.IEnumerator SpawnFoodOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Si hay menos alimentos que el máximo, generar uno nuevo
            if (currentFoodCount < maxFood)
            {
                SpawnFood();
            }
        }
    }

    private void SpawnFood()
    {
        // Obtener una posición aleatoria dentro del área de spawn
        Vector2 randomPosition = GetRandomPositionInArea();

        // Instanciar el alimento en la posición aleatoria
        Instantiate(foodPrefab, randomPosition, Quaternion.identity);

        // Incrementar el contador de alimentos
        currentFoodCount++;
    }

    private Vector2 GetRandomPositionInArea()
    {
        // Obtener los límites del área de spawn
        Bounds bounds = spawnArea.bounds;

        // Generar una posición aleatoria dentro de los límites
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }

    // Método para reducir el contador de alimentos cuando se recolecta uno
    public void FoodCollected()
    {
        currentFoodCount--;
    }
}
