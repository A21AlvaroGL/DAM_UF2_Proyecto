using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI ballCounterTxt;
    private int ballCounter = 3;
    private int score;
    private int maxScore = 999999;
    private List<MagicCircleController> magicCircles = new List<MagicCircleController>();
    [SerializeField] private Sprite desactivatedCircle;
    [SerializeField] private GameObject dragon;
    private GameObject dragonInstance;
    [SerializeField] private Transform dragonSpawnPoint;
    private GameObject portalInstace;
    [SerializeField] private GameObject portal;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private float dragonSpeed = 2f;
    private const float MIN_X = -9f;
    private const float MAX_X = 5f;
    private float moveDirection = 1f;

    void Start()
    {
        ballCounterTxt.text = string.Format("x{0}", ballCounter);
    }

    void Update()
    {
        if (dragonInstance != null && portalInstace != null)
        {
            MoveDragon();
        }
    }


    // Los siguientes 2 métodos son para implementar un singleton
    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Actualizo la UI con la puntuación siempre y cuando sea menor a la puntuación máxima
    public void UpdateScore(int points)
    {
        if ((score + points) < maxScore)
        {
            score += points;
            scoreTxt.text = string.Format("{0,4:D4}", score);
        }
        else
        {
            score = maxScore;
            scoreTxt.text = string.Format("{0,4:D4}", score);
        }
    }

    public void UpdateBalls()
    {
        ballCounter--;
        ballCounterTxt.text = string.Format("x{0}", ballCounter);

        if (ballCounter == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    /* Metodo para activar los círculos mágicos. Reproduce el sonido, aumenta la puntuación y cambia el sprite.
        Cuando la lista tiene 4 círculos mágicos activados dentro invoca al dragón*/
    public void ActivateMagicCircle(MagicCircleController magicCircle, AudioSource magicCircleAudio, Sprite activatedCircle)
    {

        if (!magicCircle.activated)
        {
            magicCircleAudio.Play();
            magicCircle.GetComponentInChildren<SpriteRenderer>().sprite = activatedCircle;

            magicCircle.changeActive();
            UpdateScore(1000);

            magicCircles.Add(magicCircle);
        }

        if (magicCircles.Count == 4)
        {
            SpawnDragon();
        }
    }

    // Desactiva todos los círculos mágicos para poder enfrentarte al dragón de nuevo
    public void DesactivateMagicCircles()
    {
        // Copio la lista de círculos mágicos para así poder borrar la original
        List<MagicCircleController> magicCirclesToRemove = new List<MagicCircleController>();

        // Cambio es sprite y el estado de todos los círculos mágicos
        foreach (MagicCircleController magicCircle in magicCircles)
        {
            magicCircle.GetComponentInChildren<SpriteRenderer>().sprite = desactivatedCircle;
            magicCircle.changeActive();

            magicCirclesToRemove.Add(magicCircle);
        }

        // Borro todos los círculos mágicos de la lista
        foreach (MagicCircleController magicCircle in magicCirclesToRemove)
        {
            magicCircles.Remove(magicCircle);
        }
    }

    public void SpawnDragon()
    {
        dragonInstance = Instantiate(dragon, dragonSpawnPoint);
        portalInstace = Instantiate(portal, dragonSpawnPoint);
    }

    public void KillDragon()
    {
        audioSource.Play();

        // Desactivo el sprite y el collider para que parezca que se ha borrado
        dragonInstance.GetComponentInChildren<SpriteRenderer>().enabled = false;
        dragonInstance.GetComponentInChildren<PolygonCollider2D>().enabled = false;

        // Cuando ya se ha reproducido el sonido lo borro de verdad
        Destroy(dragonInstance, audioSource.clip.length);
        Destroy(portalInstace);

        DesactivateMagicCircles();
    }

    // Método para mover el dragón y el portal
    private void MoveDragon()
    {
        // Calculo la posición del dragón
        float newX = dragonInstance.transform.position.x + moveDirection * dragonSpeed * Time.deltaTime;

        // Cuando alcanzo un límite del mapa cambio la dirección
        if (newX <= MIN_X || newX >= MAX_X)
        {
            moveDirection *= -1;
        }

        // Muevo el dragón y el portal
        dragonInstance.transform.position = new Vector3(newX, dragonInstance.transform.position.y, dragonInstance.transform.position.z);
        portalInstace.transform.position = new Vector3(newX, portalInstace.transform.position.y, portalInstace.transform.position.z);
    }
}