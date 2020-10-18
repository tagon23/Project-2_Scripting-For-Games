using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileSpawner : MonoBehaviour
{
    // Assign missile prefab.
    [SerializeField]
    private GameObject missile;

    // Access-point for Box script.
    private static missileSpawner instance;

    // Spawn the box when the game Starts.
    void Start()
    {
        instance = this;
        StartCoroutine(SpawnBox(0f));
    }

    IEnumerator SpawnBox(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(missile, transform.position, transform.rotation);
    }

    public static void Respawn(float respawnTime)
    {
        instance.StartCoroutine(instance.SpawnBox(respawnTime));
    }
}