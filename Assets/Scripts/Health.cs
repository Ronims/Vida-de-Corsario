using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image healthBarFill; // A imagem da barra (tipo um Slider, mas pode ser Image simples).

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
