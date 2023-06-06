using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public InputController inputController;
    public Canvas pausa;
    public static bool Pausa = true;
    public bool a;
    public Canvas menu, config;

    void Start()
    {
        
    }

    void Update()
    {
        if (inputController.Pausa() && Pausa)
        {
            pausa.enabled = true;
            Time.timeScale = 0.0f;
        }

        if (!Pausa)
        {
            StartCoroutine(espera());
        }
        a = Pausa;
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(1.0f);
        Pausa = true;
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

    public void Configuracao()
    {
        menu.enabled = false;
        config.enabled = true;
    }

    public void Voltar()
    {
        config.enabled = false;
        menu.enabled = true;

    }
}
