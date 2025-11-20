using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class RaceMonitor : MonoBehaviour
{
    public GameObject[] countDownItems;
    public ControllerRace[] CPM;
    public static bool racing = false;

    [SerializeField]
    public static int totalLaps = 1;

    public GameObject gameOverPanel;
    
    public GameObject[] carPrefabs;
    public GameObject[] carIAPrefabs;

    int playerCar;

    public Transform[] spawnPos;
    Vector3 startPos;
    Quaternion startRot;

    private GameObject pCar;

    void Start()
    {
        pCar = null;

        // Apagar UI inicial
        foreach (GameObject g in countDownItems)
            g.SetActive(false);

        gameOverPanel.SetActive(false);

        // Elegir auto del jugador
        playerCar = PlayerPrefs.GetInt("PlayerCar");

        // Seleccionar posición aleatoria de spawn
        int randomStart = Random.Range(0, spawnPos.Length);
        startPos = spawnPos[randomStart].position;
        startRot = spawnPos[randomStart].rotation;

        // Instanciar auto del jugador
        pCar = Instantiate(carPrefabs[playerCar], startPos, startRot);

        // Activar scripts del jugador
        //CamaraFindObject.carbody = pCar.GetComponent<Drive>().camPos.transform;
        pCar.GetComponent<Player>().enabled = true;
       // pCar.GetComponent<PlayerController>().enabled = true;

        // Instanciar IA en las demás posiciones
        foreach (Transform t in spawnPos)
        {
            if (t == spawnPos[randomStart]) continue;

//            GameObject aiCar = Instantiate(carIAPrefabs[Random.Range(0, carIAPrefabs.Length)]);
//            aiCar.transform.position = t.position;
//            aiCar.transform.rotation = t.rotation;
        }

        // Iniciar carrera
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(PlayCountdown());

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Player");
        CPM = new ControllerRace[cars.Length];

        for (int i = 0; i < cars.Length; i++)
        {
            CPM[i] = cars[i].GetComponent<ControllerRace>();
        }
    }

    IEnumerator PlayCountdown()
    {
        yield return new WaitForSeconds(2);

        foreach (GameObject g in countDownItems)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(1);
            g.SetActive(false);
        }
        racing = true;
    }

    public void RestartLevel()
    {
        racing = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LateUpdate()
    {
        if (!racing) return;

        int finishedCount = 0;

        foreach (ControllerRace cpm in CPM)
        {
            if (cpm.laps >= totalLaps)
                finishedCount++;
        }

        if (finishedCount == CPM.Length)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
