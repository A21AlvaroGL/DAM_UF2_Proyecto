using UnityEngine;

public class GemController : MonoBehaviour
{
    private GemsController spawn;
    [SerializeField] private AudioSource audioSource;

    public void SetSpawn(GemsController spawn)
    {
        this.spawn = spawn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            audioSource.Play();
            GameManager.GetInstance().UpdateScore(200);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            spawn.GemCollected(gameObject, audioSource.clip.length);
        }
    }
}
