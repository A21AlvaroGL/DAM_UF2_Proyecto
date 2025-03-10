using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;

    void Start()
    {
        // Aumento la puntuación cada 0.25 segundos
        InvokeRepeating("AddScore", 1f, 0.25f);
    }

    // Update is called once per frame

    void Update()
    {
        /* Primero calculo el tiempo y lo guardo en elapsedTime, despues calculo los minutos y los segundos.
            Por último, lo formateo y lo asigno como el texto del TextMeshPro de la UI */
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Aumenta en uno la puntuación
    void AddScore()
    {
        GameManager.GetInstance().UpdateScore(1);
    }

}
