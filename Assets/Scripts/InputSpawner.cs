using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _spawnObjects = null;
    [SerializeField] Transform[] _spawnLocations = null;

    [SerializeField] float _spawnedObjectLifetime = 1.5f;

    [Header("Feedback")]
    [SerializeField] ParticleSystem _spawnParticle = null;
    [SerializeField] AudioClip _spawnSFX = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        // calculate randomization
        int randomObjectNumber = Random.Range(0, _spawnObjects.Length);
        int randomLocationNumber = Random.Range(0, _spawnLocations.Length);
        // calculate random spawn transform, for readability
        Transform spawnLocation = _spawnLocations[randomLocationNumber];

        // spawn gameObject
        GameObject spawnedObject = Instantiate(_spawnObjects[randomObjectNumber],
            spawnLocation.position, spawnLocation.rotation);
        Destroy(spawnedObject, _spawnedObjectLifetime);

        PlaySpawnFeedback(spawnLocation);
    }

    void PlaySpawnFeedback(Transform spawnLocation)
    {
        // spawn particles
        ParticleSystem particleSystem = Instantiate(_spawnParticle,
            spawnLocation.position, spawnLocation.rotation);
        // audio
        AudioSource.PlayClipAtPoint(_spawnSFX, spawnLocation.position);
    }

}
