using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public InputController inputController;
    public Canvas pausa;

    void Start()
    {
        
    }

    void Update()
    {
        if (inputController.Pausa())
        {
            pausa.enabled = true;
            Time.timeScale = 0.0f;
        }
    }

    public void Continuar()
    {
        Time.timeScale = 1.0f;
        pausa.enabled = false;
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void Sair()
    {
        Time.timeScale = 1.0f;
        pausa.enabled = false;
        SceneManager.LoadScene(0);
    }
}
