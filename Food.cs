using UnityEngine;

public class Food : MonoBehaviour
{
    private FoodSpawner spawner;

    void Start()
    {
        // Buscar el FoodSpawner en la escena
        spawner = FindObjectOfType<FoodSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            // Notificar al spawner que este alimento fue recolectado
            spawner.FoodCollected();

            // Destruir el alimento
            Destroy(gameObject);
        }
    }
}
