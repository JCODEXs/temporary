using UnityEngine;

public class PlayerFood : MonoBehaviour
{
    public int maxFood = 5; // Nivel máximo de Food del personaje
    public int currentFood;   // Nivel actual de Food del personaje

    [Header("Daño y curación")]
    public int foodHealAmount = 1; // Cantidad de Food que aumenta un alimento
    public int obstacleDamageAmount = 1; // Cantidad de Food que reduce un obstáculo
    public int timeDamageAmount = 1; // Cantidad de Food reducida por intervalo de tiempo

    [Header("Tiempo")]
    public float timeDamageInterval = 180f; // Intervalo de tiempo para reducir el Food

    private float timeSinceLastDamage;
    private PlayerAnimationController animationController;

    void Start()
    {
        currentFood = maxFood;
        animationController = GetComponent<PlayerAnimationController>();
        timeSinceLastDamage = 0f;
    }

    void Update()
    {
        HandleDamageOverTime();
    }

    private void HandleDamageOverTime()
    {
        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= timeDamageInterval)
        {
            TakeDamage(timeDamageAmount);
            timeSinceLastDamage = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        currentFood -= damage;
        currentFood = Mathf.Clamp(currentFood, 0, maxFood);
        animationController.PlayDamageAnimation(); // Reproducir animación de recibir daño

        if (currentFood <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentFood += amount;
        currentFood = Mathf.Clamp(currentFood, 0, maxFood);
        animationController.PlayHealAnimation(); // Reproducir animación de comer
    }

    private void Die()
    {
        Debug.Log("El personaje ha muerto.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Heal(foodHealAmount); // Aumentar el Food al recoger un alimento
            Destroy(collision.gameObject); // Destruir el objeto de alimento
        }
        else if (collision.CompareTag("Obstacle"))
        {
            TakeDamage(obstacleDamageAmount); // Reducir el Food al chocar con un obstáculo
        }
    }
}
