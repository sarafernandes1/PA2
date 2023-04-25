using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool gameOver = false;
    public Canvas canvas;

    private void Update()
    {
        if (gameOver)
        {
            canvas.enabled = true;
            Time.timeScale = 0.0f;
        }
    }

    public void Recomecar()
    {
        Time.timeScale = 1.0f;
        gameOver = false;
        SceneManager.LoadScene(1);
    }

    public void Sair()
    {
        Time.timeScale = 1.0f;
        gameOver = false;

        SceneManager.LoadScene(0);

    }
}
