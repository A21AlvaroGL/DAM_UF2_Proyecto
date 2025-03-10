using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Cargo la escena con el juego
    public void PlayGame() {
        // Si ya hay un game manager creado lo borro 
        GameManager gameManager = GameManager.GetInstance();
        if (gameManager != null)
        {
            Destroy(gameManager.gameObject);
        }

        SceneManager.LoadScene("MainScene");
    }

    // Cierro el juego (funciona si es la versión compilada del juego desde el editor no)
    public void QuitGame() {
        Application.Quit();
    }
}
