using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject explosionEffect;  // Efeito ao acertar barco
    public GameObject splashEffect;     // Efeito ao acertar água

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Cannonball colidiu com: " + collision.gameObject.name);

        // Colisão com barco (ou qualquer objeto sólido)
        if (collision.gameObject.CompareTag("Barco") || collision.gameObject.CompareTag("EnemyShip") || collision.gameObject.CompareTag("BarcoJogador") || collision.gameObject.CompareTag("Cenario"))
        {
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
         if ( collision.gameObject.CompareTag("Peixe"))
        {
            Debug.Log("Acertou o tutuba");
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Instantiate(splashEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 2f);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cannonball entrou em Trigger: " + other.gameObject.name);

        // Ao tocar na água
        if (other.gameObject.CompareTag("Water Volume"))
        {
            if (splashEffect != null)
            {
                Instantiate(splashEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 2f);
        }
    }
}
