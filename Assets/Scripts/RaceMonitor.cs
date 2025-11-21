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
        int finishedCount = 0;
        foreach (GameObject g in npcs)
        {
            g.GetComponent<NPC_player>().enabled = true;
        }
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

    public void PauseRace()
    {
        Time.timeScale = 0;
    }

    public void ResumeRace()
    {
        Time.timeScale = 1;
    }
}
