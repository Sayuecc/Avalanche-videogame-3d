using UnityEngine;

public class ControlPausaSimple : MonoBehaviour
{
    
    public static bool isGamePaused = false;

   
    public GameObject pauseMenuUI;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume(); 
            }
            else
            {
                Pause(); 
            }
        }
    }

    
    public void Resume()
    {
        pauseMenuUI.SetActive(false); /
        Time.timeScale = 1f;          
        isGamePaused = false;
    }

    
    void Pause()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;         
        isGamePaused = true;
    }
} 