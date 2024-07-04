using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratesSpawner : MonoBehaviour
{
    [Header("Spawnpoints positions")]
    [SerializeField] private Transform[] spawnpoints;

    [Header("Crate GameObject")]
    [SerializeField] private GameObject Crate;

    [Header("Time properties")]
    [SerializeField] private float crateSpawnRate;

    private float _spawnCounter;
    private bool _isGamePaused;

    private void Start()
    { 
        _spawnCounter = 0;
        _isGamePaused = GameManager.IsGamePaused;
    }

    private void Update()
    {
        //if (!_isGamePaused && GameManager.Instance.isLevelStarted)
        if (!_isGamePaused && GameManager.Instance.IsGameplayActive)
        {
            _spawnCounter += Time.deltaTime;

            if (_spawnCounter >= crateSpawnRate)
            { 
                SpawnCrate();
                _spawnCounter = 0;
            }
        }
    }

    private void SpawnCrate()
    {
        Instantiate(Crate, spawnpoints[Random.Range(0, spawnpoints.Length - 1)].position, Quaternion.identity);
    }
}
