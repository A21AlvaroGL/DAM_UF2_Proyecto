using UnityEngine;

public class FlipperController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float angle = 65f;

    private Rigidbody2D rb;
    private float originalRotation;
    private float targetAngle;
    private KeyCode assignedKey;
    private KeyCode assignedKeySecondary;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalRotation = transform.rotation.eulerAngles.z;

        // Le asigno los controles a cada flipper y le indico hasta dónde quiero que roten
        if (CompareTag("FlipperLeft"))
        {
            assignedKey = KeyCode.A;
            assignedKeySecondary = KeyCode.LeftArrow;
            targetAngle = originalRotation + angle;
        }
        else if (CompareTag("FlipperRight"))
        {
            assignedKey = KeyCode.D;
            assignedKeySecondary = KeyCode.RightArrow;
            targetAngle = originalRotation - angle;
        }
        else
        {
            Debug.LogError("El flipper no tiene un tag correcto");
        }
    }

    void Update()
    {
        // Roto el flipper y lo devuelvo a su posición inicial
        if (Input.GetKey(assignedKey) || Input.GetKey(assignedKeySecondary))
        {
            rotateFlipper(targetAngle);
        }
        else
        {
            rotateFlipper(originalRotation);
        }

        // Reproduzco el sonido solo cuando se presiona la tecla
        if (Input.GetKeyDown(assignedKey) || Input.GetKeyDown(assignedKeySecondary))
        {
            audioSource.Play();
        }
    }

    // Función encargada de rotar el flipper
    void rotateFlipper(float newAngle)
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, newAngle, speed * Time.fixedDeltaTime));
    }
}
