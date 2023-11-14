using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    #region Level1UI
    [SerializeField] GameObject gameplayUI;
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
    //Menu de pausa
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado = false;
    void Start()
    {
        #region Level1UI
        //defeatScreen = transform.GetChild(2).gameObject;
        gameplayUI.SetActive(true);
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
        if (initScreen.canvasGroup.alpha == 1 && GameManager.Instance.isLevel1Completed)
        {
            EventManager.OnKilledEnemy -= EnemyKilled;
            GameManager.Instance.ChangeLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Y)) GameManager.Instance.isLevel1Completed = true;
        //Menu de pausa
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado) Reanudar();
            else Pausa();
        }
    }

    void GetEnemiesAmount()
    {
        enemiesToDefeat = 0;
        for (int i = 0; i <= enemiesOnStage.Length - 1; i++)
            enemiesToDefeat++;
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
        SceneManager.LoadScene(0);
        //Application.Quit();
    }

    void RefreshHPBar()
    {
        hpFill.fillAmount = player.PlayerHealth / 100f;
    }
    #endregion
    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }
    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
    public void Reiniciar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
