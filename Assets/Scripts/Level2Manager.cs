using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject[] enemies;
    [SerializeField]
    InitialScreen initScreen;

    float enemyTimer;
    float timerChanger;
    float boxTimer;
    public float counter;

    void Start()
    {
        initScreen.isLevelEnded = false;
        counter = 30;
        timerChanger = 3;
        enemyTimer = 4;
        EventManager.OnKilledEnemy += EnemyKilled;
    }

    void Update()
    {
        if (counter <= 0)
        {
            initScreen.isLevelEnded = true;
            if (initScreen.GetComponent<CanvasGroup>().alpha == 1)
            {
                GameManager.Instance.ChangeLevel(3);
            }
        }

        if (!GameManager.isGamePaused && initScreen.canvasGroup.alpha <= 0)
        {
            enemyTimer += Time.deltaTime;
            boxTimer += Time.deltaTime;

            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }

            if (enemyTimer >= timerChanger)
            {
                Instantiate(enemies[Mathf.RoundToInt(Random.Range(0, 3))],
                            spawnPoints[Mathf.RoundToInt(Random.Range(0, 6))].position,
                            transform.rotation);
                enemyTimer = 0;
            }

            if (boxTimer >= 2)
            {
                Instantiate(enemies[3],
                            spawnPoints[Mathf.RoundToInt(Random.Range(0, 6))].position,
                            transform.rotation);
                boxTimer = 0;
            }
        }
    }

    public void EnemyKilled()
    {
        if (timerChanger > 0.8f)
        {
            timerChanger -= 0.1f;
        }
    }
}
