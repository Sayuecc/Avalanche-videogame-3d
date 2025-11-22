using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class RaceMonitor : MonoBehaviour
{
    public GameObject[] countDownItems;
    public ControllerRace[] CPM;
    public  bool racing = false;
    public  int totalLaps = 1;

    public GameObject gameOverPanel;
    public GameObject winText;
    public GameObject loseText;
    
    public GameObject[] carPrefabs;
    public GameObject[] carIAPrefabs;
    public List<GameObject> npcs;
    int playerCar;
    public Transform[] Waypoints;

    public Transform[] spawnPos;
    Vector3 startPos;
    Quaternion startRot;

    private GameObject pCar;
public bool startGame = false;
public static RaceMonitor Instance;

private void Awake()
{
    Instance = this;
}
    void Start()
    {
        npcs = new List<GameObject>();
        pCar = null;
        // Elegir auto del jugador
        playerCar = PlayerPrefs.GetInt("PlayerCar");
        // Seleccionar posición aleatoria de spawn
        int randomStart = Random.Range(0, spawnPos.Length);
        startPos = spawnPos[randomStart].position;
        startRot = spawnPos[randomStart].rotation;
        // Instanciar jugador
        pCar = Instantiate(carPrefabs[playerCar], startPos, startRot);
        // Activar scripts del jugador
        pCar.GetComponent<Player>().enabled = false;
        // Instanciar IA en las demás posiciones
        foreach (Transform t in spawnPos)
        {
            if (t == spawnPos[randomStart]) continue;
            GameObject aiCar = Instantiate(carIAPrefabs[Random.Range(0, carIAPrefabs.Length)]);
            aiCar.GetComponent<NPC_player>().SetWaypoints(Waypoints);
            aiCar.GetComponent<NPC_player>().enabled = false;
            aiCar.transform.position = t.position;
            aiCar.transform.rotation = t.rotation;
            npcs.Add(aiCar);
        }
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
        pCar.GetComponent<Player>().enabled = true;
        RaceLeaderboard.Instance.RegisterRunner(pCar.GetComponent<ControllerRace>());
        RaceLeaderboard.Instance.player = pCar.GetComponent<ControllerRace>();
        int finishedCount = 0;
        foreach (GameObject g in npcs)
        {
            g.GetComponent<NPC_player>().enabled = true;
            RaceLeaderboard.Instance.RegisterRunner(g.GetComponent<ControllerRace>());
        }
        foreach (ControllerRace cpm in CPM)
        {
            if (cpm.laps >= totalLaps)
                finishedCount++;
        }

        if (finishedCount == CPM.Length)
        {
            racing = false;

            int finalPos = RaceLeaderboard.Instance.GetPlayerFinalPosition();

            if (finalPos == 1)
            {
                // GANASTE
                gameOverPanel.SetActive(true); // aquí va tu pantalla de victoria
                winText.SetActive(true); 
                Debug.Log("GANASTE! Quedaste en primera posición");
            }
            else
            {
                // PERDISTE
                gameOverPanel.SetActive(true); // si es otra pantalla para perder, usa otro panel
                loseText.SetActive(true); 
                Debug.Log("PERDISTE! Quedaste en la posición " + finalPos);
            }
        }
    }

    public void PauseRace()
    {
        Time.timeScale = 0;
    }

    public void ResumeRace()
    {
        Time.timeScale = 1;
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
