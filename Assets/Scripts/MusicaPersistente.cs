using UnityEngine;

public class MusicaPersistente : MonoBehaviour
{
    private void Awake()
    {
       
        DontDestroyOnLoad(gameObject);

        
        var musicas = FindObjectsOfType<MusicaPersistente>();
        if (musicas.Length > 1)
            Destroy(gameObject);
    }
}
