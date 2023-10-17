using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    public int enemiesToDefeat;

    public bool levelCompleted;

    public InitialScreen initScreen;

    GameObject[] enemiesOnStage;

    [SerializeField] Player player;

    public Text enemyCounter;

    void Start()
    {
        GameManager.Instance.isLevel1Completed = false;

        enemiesOnStage = GameObject.FindGameObjectsWithTag("Enemy");

        GetEnemiesAmount();

        enemyCounter = GameObject.FindGameObjectWithTag("Counter").GetComponent<Text>();

        enemyCounter.text = enemiesToDefeat.ToString();

        EventManager.OnKilledEnemy += EnemyKilled;
    }

    void Update()
    {
        if (initScreen.canvasGroup.alpha == 1 && GameManager.Instance.isLevel1Completed)
        {
            EventManager.OnKilledEnemy -= EnemyKilled;
            GameManager.Instance.ChangeLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Y)) GameManager.Instance.isLevel1Completed = true;
    }

    void GetEnemiesAmount()
    {
        enemiesToDefeat = 0;

        for (int i = 0; i <= enemiesOnStage.Length - 1; i++)
        {
            enemiesToDefeat++;
        }
    }

    public void EnemyKilled()
    {
        enemiesToDefeat--;
        if (enemyCounter == null) enemyCounter = GameObject.FindGameObjectWithTag("Counter").GetComponent<Text>();
        enemyCounter.text = enemiesToDefeat.ToString();
        if (enemiesToDefeat == 0) GameManager.Instance.isLevel1Completed = true;
    }

    private void OnDisable()
    {
        UnSuscribe();
    }

    void UnSuscribe()
    {
        EventManager.OnKilledEnemy -= EnemyKilled;
    }
}
