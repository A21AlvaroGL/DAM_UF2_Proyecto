using UnityEngine;

public class SpringController : MonoBehaviour
{
    [SerializeField] private float springForce = 100f;
    [SerializeField] private float compressionSpeed = 10f;
    [SerializeField] private float releaseSpeed = 100f;

    private Rigidbody2D rb;
    private Rigidbody2D ball;
    private Vector2 originalPosition;
    private Vector2 compressedPosition;
    private bool isReleasing = false;
    private bool isBallTouchingSpring = false;
    [SerializeField] private AudioSource compressSpringAudio;
    [SerializeField] private AudioSource releaseSpringAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originalPosition = transform.position;
        compressedPosition = new Vector2(transform.position.x, transform.position.y - 3f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            CompressSpring();
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            isReleasing = true;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            compressSpringAudio.Play();
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            releaseSpringAudio.Play();
        }

    }

    void FixedUpdate()
    {
        if (isReleasing)
        {
            ReleaseSpring();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ball = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
        if (other.CompareTag("Ball"))
        {
            isBallTouchingSpring = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            isBallTouchingSpring = false;
        }
    }

    // Muevo el resorte hacia atrás en el eje y a una determinada velocidad
    void CompressSpring()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, compressedPosition, compressionSpeed * Time.deltaTime));
    }

    // Devuelvo el resorte a su posición original 
    void ReleaseSpring()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, originalPosition, releaseSpeed * Time.deltaTime));

        if (Vector2.Distance(rb.position, originalPosition) < 0.1f)
        {
            isReleasing = false;

            if (isBallTouchingSpring)
            {
                ball.AddForce(Vector2.up * springForce);
            }
        }
    }
}
