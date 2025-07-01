using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class WeightedEventPrefab
{
    public GameObject prefab;
    [Range(0f, 1f)] public float weight = 0.2f; // Default weight
}

public enum EventType
{
    Combat,
    Trader,
    Situation,
    TarotCards,
    Treasure,
    // Add more as needed
}

public class Generate4Rooms : MonoBehaviour
{
    [Header("Assign event prefabs and their chances (weights)")]
    public List<WeightedEventPrefab> weightedEventPrefabs;
    [Header("How many events to generate?")]
    public int numberOfEvents = 4;

    private int currentEventIndex = 0;
    private GameObject currentEventInstance;
    private List<GameObject> selectedPrefabs = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (weightedEventPrefabs == null || weightedEventPrefabs.Count == 0)
        {
            Debug.LogError("No event prefabs assigned to Generate4Rooms!");
            return;
        }
        if (numberOfEvents <= 0)
        {
            Debug.LogError("Number of events must be greater than 0!");
            return;
        }
        // Select random prefabs based on weights
        for (int i = 0; i < numberOfEvents; i++)
        {
            selectedPrefabs.Add(GetRandomPrefabByWeight());
        }
        SpawnEvent(0);
    }

    WeightedEventPrefab GetRandomWeightedEvent()
    {
        float totalWeight = 0f;
        foreach (var wep in weightedEventPrefabs)
            totalWeight += wep.weight;
        float rand = Random.Range(0f, totalWeight);
        float cumulative = 0f;
        foreach (var wep in weightedEventPrefabs)
        {
            cumulative += wep.weight;
            if (rand <= cumulative)
                return wep;
        }
        return weightedEventPrefabs[weightedEventPrefabs.Count - 1]; // fallback
    }

    GameObject GetRandomPrefabByWeight()
    {
        return GetRandomWeightedEvent().prefab;
    }

    void SpawnEvent(int index)
    {
        if (currentEventInstance != null)
        {
            Destroy(currentEventInstance);
        }
        if (index >= selectedPrefabs.Count)
        {
            Debug.Log("All events completed!");
            return;
        }
        GameObject prefab = selectedPrefabs[index];
        Debug.Log("Spawning event at index: " + index + " with prefab: " + prefab.name);
        currentEventInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity, this.transform);
    }

    // Call this method from your event prefab when the event is completed
    public void OnEventCompleted()
    {
        currentEventIndex++;
        Debug.Log("OnEventCompleted called. New index: " + currentEventIndex);
        if (currentEventIndex < selectedPrefabs.Count)
        {
            SpawnEvent(currentEventIndex);
        }
        else
        {
            Debug.Log("All events completed!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
