using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider sliderVolumen;

    void Start()
    {
        
        float volumenGuardado = PlayerPrefs.GetFloat("VolumenGeneral", 1f);
        sliderVolumen.value = volumenGuardado;
        AudioListener.volume = volumenGuardado;
    }

    public void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("VolumenGeneral", valor);
        PlayerPrefs.Save();
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}

