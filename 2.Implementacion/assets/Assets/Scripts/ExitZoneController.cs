using TMPro.Examples;
using UnityEngine;

public class ExitZoneController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        SpawnBall();
    }

    // Cuando la bola sale del mapa la borra y crea una nueva
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            audioSource.Play();
            Destroy(collision.gameObject);
            GameManager.GetInstance().UpdateBalls();
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        GameObject newBall = Instantiate(ball, ballSpawnPoint);

        // Asigno la bola a la camara para que esta la siga
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.target = newBall.transform;
    }
}
