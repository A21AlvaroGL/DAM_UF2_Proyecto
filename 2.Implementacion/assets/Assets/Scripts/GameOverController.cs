using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Si se pulsa cualquier tecla cargo la escena del menú
    void Update()
    {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
