using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Sprite fireBall;
    [SerializeField] private AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Cuando el dragón es golpeado por la bola le resto vida y reproduzco el sonido
        if (collision.CompareTag("Ball"))
        {
            health -= 20;
            audioSource.Play();

            // Cuando derrotas al dragón aumento la puntuación, lo elimino y cambio la bola
            if (health == 0)
            {
                GameManager.GetInstance().UpdateScore(25000);
                GameManager.GetInstance().KillDragon();
                ChangeBall(collision.gameObject);
            }
        }
    }

    // Cambio la bola actual por la versión de fuego
    public void ChangeBall(GameObject ball)
    {
        ball.GetComponentInChildren<SpriteRenderer>().sprite = fireBall;
    }
}
