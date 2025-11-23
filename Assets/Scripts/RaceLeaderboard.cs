using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RaceLeaderboard : MonoBehaviour
{
    public List<ControllerRace> racers; // Player + NPCs
    public Transform[] checkpoints; // Lista de transforms de checkpoints EN ORDEN
    public static RaceLeaderboard Instance;
    public TextMeshProUGUI[] positionTexts; // Asigna tus 4 textos aquí
    public ControllerRace player; // Asigna el Player instanciado

    
    private void Awake()
    {
        Instance = this;
    }
    
    public void RegisterRunner(ControllerRace runner)
    {
        if (!racers.Contains(runner))
            racers.Add(runner);
    }
    void Update()
    {
        SortLeaderboard();
    }

    void SortLeaderboard()
    {
        var ordered = racers
            .OrderByDescending(r => r.laps)
            .ThenByDescending(r => r.currentCheckpoint)
            .ThenBy(r => DistanceToNextCheckpoint(r))
            .ToList();

        // Mostrar los primeros 4 en los textos
        for (int i = 0; i < positionTexts.Length; i++)
        {
            if (i < ordered.Count)
            {
                string cleanName = ordered[i].name.Replace("(Clone)", "").Trim();

                positionTexts[i].text = (i + 1) + ". " + cleanName;
            }
            else
            {
                positionTexts[i].text = "";
            }
        }

        // Debug opcional
        for (int i = 0; i < ordered.Count; i++)
        {
            Debug.Log($"{i+1}. {ordered[i].name}   Laps:{ordered[i].laps}   CP:{ordered[i].currentCheckpoint}");
        }
    }
    public int GetPlayerFinalPosition()
    {
        var ordered = racers
            .OrderBy(r => r.finished ? 0 : 1)  // primero los no terminados
            .ThenBy(r => r.finished ? r.finishTime : 0) // los que terminaron por tiempo
            .ThenByDescending(r => r.laps) // si no terminaron, ordenar por progreso
            .ThenByDescending(r => r.currentCheckpoint)
            .ThenBy(r => DistanceToNextCheckpoint(r))
            .ToList();


        return ordered.IndexOf(player) + 1; // devuelve 1,2,3...
    }


    float DistanceToNextCheckpoint(ControllerRace r)
    {
        int next = r.currentCheckpoint % checkpoints.Length;
        return Vector3.Distance(r.transform.position, checkpoints[next].position);
    }
}