using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CombatScenario
{
    public string scenarioName;
    public GameObject[] creaturePrefabs;
}

public class CombatEvent : MonoBehaviour
{
    public CombatScenario[] scenarios;
    private CombatScenario chosenScenario;

    public List<Transform> spawnPositions; // Assign 5 spawn point objects in inspector

    public int enemiesToDefeat = 3;
    private int enemiesDefeated = 0;

    // Called by Generate4Rooms to activate this event
    public void ActivateEvent()
    {
        // Randomly pick a scenario
        chosenScenario = scenarios[Random.Range(0, scenarios.Length)];

        // Instantiate creatures at spawn positions in order
        for (int i = 0; i < chosenScenario.creaturePrefabs.Length; i++)
        {
            if (i < spawnPositions.Count && spawnPositions[i] != null)
            {
                Instantiate(chosenScenario.creaturePrefabs[i], spawnPositions[i].position, spawnPositions[i].rotation);
            }
            else
            {
                Instantiate(chosenScenario.creaturePrefabs[i], transform.position, transform.rotation);
            }
        }

        // Set enemiesToDefeat based on scenario
        enemiesToDefeat = chosenScenario.creaturePrefabs.Length;
        enemiesDefeated = 0;
    }

    // Call this when an enemy is defeated
    public void OnEnemyDefeated()
    {
        enemiesDefeated++;
        if (enemiesDefeated >= enemiesToDefeat)
        {
            CompleteCombat();
        }
    }

    void CompleteCombat()
    {
        // Notify Generate4Rooms that this event is complete
        FindFirstObjectByType<Generate4Rooms>().OnEventCompleted();
        // Optionally, destroy this event prefab
        Destroy(gameObject);
    }

    void Start()
    {
        ActivateEvent();
    }
} 