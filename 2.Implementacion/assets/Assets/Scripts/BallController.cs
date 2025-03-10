using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 100f;
    private Rigidbody2D ball;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Esto añade una velocidad máxima a la bola
        if (ball.linearVelocity.magnitude > maxSpeed)
        {
            ball.linearVelocity = ball.linearVelocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
}
