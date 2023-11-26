using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    #region Level2UI
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] Text timer;
    [SerializeField] Player player;
    [SerializeField] Image playerFill;
    #endregion
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    InitialScreen initScreen;
    public EnemiesOnLevelData ScriptableObj;
    float enemyTimer;
    float timerChanger;
    float boxTimer;
    public float counter;
    //Menu de pausa
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado = false;

    private void Awake()
    {
        //defeatScreen = transform.GetChild(2).gameObject; //Level2UI
    }
    void Start()
    {
        initScreen.isLevelEnded = false;
        counter = 30;
        timerChanger = 3;
        enemyTimer = 4;
        EventManager.OnKilledEnemy += EnemyKilled;
        #region Level2UI
        //defeatScreen = transform.GetChild(2).gameObject;
        timer.text = "30";
        gameplayUI.SetActive(true);
        defeatScreen.SetActive(false);
        EventManager.OnPlayerDefeat += DefeatScreen;
        EventManager.OnHpVariation += RefreshHPBar;
        #endregion
    }

    void Update()
    {
        #region Level2UI
        if (!GameManager.IsGamePaused)
        {
            timer.text = Mathf.RoundToInt(counter).ToString();
            gameplayUI.SetActive(true);
        }
        else
        {
            gameplayUI.SetActive(false);
        }
        #endregion
        if (counter <= 0)
        {
            initScreen.isLevelEnded = true;
            if (initScreen.GetComponent<CanvasGroup>().alpha == 1) GameManager.Instance.ChangeLevel(3);
        }

        if (!GameManager.IsGamePaused && initScreen.canvasGroup.alpha <= 0)
        {
            enemyTimer += Time.deltaTime;
            boxTimer += Time.deltaTime;
            if (counter > 0) counter -= Time.deltaTime;
            if (enemyTimer >= timerChanger)
            {
                Instantiate(ScriptableObj.Enemies[Mathf.RoundToInt(Random.Range(0, 3))],
                            spawnPoints[Mathf.RoundToInt(Random.Range(0, 6))].position,
                            transform.rotation);
                enemyTimer = 0;
            }

            if (boxTimer >= 2)
            {
                Instantiate(ScriptableObj.Enemies[3],
                            spawnPoints[Mathf.RoundToInt(Random.Range(0, 6))].position,
                            transform.rotation);
                boxTimer = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado) Reanudar();
            else Pausa();
        }
    }

    public void EnemyKilled()
    {
        if (timerChanger > 0.8f) timerChanger -= 0.1f;
    }
    #region Level2UI
    

    void DefeatScreen()
    {
        if (defeatScreen == null) return;
        defeatScreen.SetActive(true);
        EventManager.OnHpVariation -= RefreshHPBar;
        EventManager.OnPlayerDefeat -= DefeatScreen;
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
        playerFill.fillAmount = player.HealthController.MaxLife / 100f;
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
