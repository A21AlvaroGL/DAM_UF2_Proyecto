using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] public float smoothSpeed = 0.125f;
    public Vector2 locationOffset;

    void FixedUpdate()
    {
        // Calculo la posición de la cámara, le aplico un suavizado y lo aplico
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y, transform.position.z) + (Vector3)locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
