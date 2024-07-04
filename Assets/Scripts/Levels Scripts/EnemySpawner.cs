using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private GameObject[] enemies;

    [Header("Weapon Types")]
    [SerializeField] private GameObject[] weapons;

    [Header("Spawn points")]
    [SerializeField] private Transform[] spawnpoints;

    [Header("Time")]
    [SerializeField] private float spawnRate;

    private float spawnCounter;

    private void Start()
    {
        spawnCounter = 0;
    }

    private void Update()
    {
        if (!GameManager.IsGamePaused && GameManager.Instance.IsGameplayActive)
        {
            spawnCounter += Time.deltaTime;

            if (spawnCounter >= spawnRate)
            { 
                SpawnCharacter();
                spawnCounter = 0;
            }
        }
    }

    private void SpawnCharacter()
    {
        int character = Random.Range(0, enemies.Length - 1);
        int weapon = Random.Range(0, weapons.Length - 1);
        int pos = Random.Range(0, spawnpoints.Length - 1);

        GameObject temp = Instantiate(enemies[character], spawnpoints[pos].position, Quaternion.identity);
        temp.GetComponent<Enemy>().SetWeapon(weapons[weapon]);
    }
}
