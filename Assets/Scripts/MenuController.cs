using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void CargarMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void CargarInicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void CargarCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void CargarAyuda()
    {
        SceneManager.LoadScene("Ayuda");
    }

    public void CargarOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void CargarSeleccionJugador()
    {
        SceneManager.LoadScene("SeleccionJugador");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
