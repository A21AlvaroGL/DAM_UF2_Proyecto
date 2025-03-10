using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        GameManager.GetInstance().UpdateScore(75);
    }
}
