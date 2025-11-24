using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public void SelectPlayer(string playerId)
    {
        
        PlayerPrefs.SetString("SelectedPlayer", playerId);
        PlayerPrefs.Save();
        Debug.Log("Jugador seleccionado: " + playerId);

        
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
