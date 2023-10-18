﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    #region Level1UI
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] Text counter;
    public Image hpFill;
    #endregion
    public int enemiesToDefeat;
    public bool levelCompleted;
    public InitialScreen initScreen;
    GameObject[] enemiesOnStage;
    [SerializeField] Player player;
    public Text enemyCounter;
    void Start()
    {
        #region Level1UI
        defeatScreen = transform.GetChild(2).gameObject;
        gameplayUI.SetActive(true);
        pauseUI.SetActive(false);
        defeatScreen.SetActive(false);
        EventManager.OnPlayerDefeat += DefeatScreen;
        EventManager.OnHpVariation += RefreshHPBar;
        #endregion
        GameManager.Instance.isLevel1Completed = false;
        enemiesOnStage = GameObject.FindGameObjectsWithTag("Enemy");
        GetEnemiesAmount();
        enemyCounter = GameObject.FindGameObjectWithTag("Counter").GetComponent<Text>();
        enemyCounter.text = enemiesToDefeat.ToString();
        EventManager.OnKilledEnemy += EnemyKilled;
    }

    void Update()
    {
        #region Level1UI
        if (GameManager.isGamePaused)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(true);
        }
        else
        {
            gameplayUI.SetActive(true);
            pauseUI.SetActive(false);
        }
        #endregion


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
    #region Level1UI
    void PauseMenu()
    {
        if (gameplayUI == null) return;
        gameplayUI.SetActive(!gameplayUI.activeSelf);
        pauseUI.SetActive(!pauseUI.activeSelf);
    }
    void DefeatScreen()
    {
        if (defeatScreen == null) return;
        defeatScreen.SetActive(true);
        EventManager.OnPlayerDefeat -= DefeatScreen;
        EventManager.OnHpVariation -= RefreshHPBar;
    }

    public void MainMenu()
    {
        EventManager.OnPlayerDefeat -= DefeatScreen;
        EventManager.OnHpVariation -= RefreshHPBar;
        GameManager.Instance.LoadMainMenu();
    }
    public void Exit()
    {
        Application.Quit();
    }

    void RefreshHPBar()
    {
        hpFill.fillAmount = player.PlayerHealth / 100f;
    }
    #endregion
}
