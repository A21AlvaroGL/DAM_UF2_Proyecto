using System;
using UnityEngine;

public class MagicCircleController : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private Sprite activateSprite;
    public bool activated = false;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Cuando la bola toca el círculo cambio el sprite para indicar que está activado.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            sprite.sprite = activateSprite;

            if (!activated)
            {
                GameManager.GetInstance().ActivateMagicCircle(this, audioSource, activateSprite);
            }
        }
    }

    public void changeActive() {
        if (activated) {
            activated = false;
        } else {
            activated = true;
        }
    }
}
