using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
{
    #region Level3UI
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject winnerScreen;
    [SerializeField] Player player;
    [SerializeField] Image bossLife;
    [SerializeField] Image playerFill;
    #endregion
    public Transform[] PatrolPoints;
    public GameObject particles;
    ParticleSystem particlesPrefab;
    public InitialScreen initScreen;
    bool isParticleUsed;
    public int bossDamage;
    public int currentDestination;
    public bool isReturning;
    int currentBossStage;
    public Scriptableobject ScriptableObj;
    //Menu de pausa
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado = false;
    void Start()
    {
        #region Level3UI
        gameplayUI.SetActive(true);
        defeatScreen.SetActive(false);
        winnerScreen.SetActive(false);
        bossLife.fillAmount = 1f;
        EventManager.OnPlayerDefeat += DefeatScreen;
        EventManager.OnHpVariation += RefreshHPBar;
        #endregion
        bossDamage = 0;
        isReturning = false;
        currentDestination = 3;
        GameManager.Instance.isBossDefeated = false;
        particlesPrefab = particles.GetComponent<ParticleSystem>();
        for (int i = 0; i < ScriptableObj.Bosses.Length; i++)
        {
            ScriptableObj.Bosses[i].SetActive(false);
        }
    }
    void Update()
    {
        #region Level3UI
        if (GameManager.Instance.isBossDefeated) winnerScreen.SetActive(true);
        bossLife.fillAmount = 1 - bossDamage / 100f;
        if (GameManager.isGamePaused) gameplayUI.SetActive(false);
        else gameplayUI.SetActive(true);

        #endregion
        if (initScreen.canvasGroup.alpha > 0) return;
        BossSetter();
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado) Reanudar();
            else Pausa();
        }
    }

    void BossSetter()
    {
        if (bossDamage >= 100)
        {
            ScriptableObj.Bosses[2].SetActive(false);
            GameManager.Instance.isBossDefeated |= true;
            return;
        }

        if (bossDamage >= 75)
        {
            StartCoroutine(BossChanger(ScriptableObj.Bosses[1], ScriptableObj.Bosses[2]));
            return;
        }

        if (bossDamage >= 50)
        {
            StartCoroutine(BossChanger(ScriptableObj.Bosses[0], ScriptableObj.Bosses[1]));
            return;
        }
        if (bossDamage < 50) ScriptableObj.Bosses[0].SetActive(true);
    }

    void ChangeBoss(GameObject defeatedBoss, GameObject newBoss)
    {
        if (!newBoss.active)
        {
            defeatedBoss.SetActive(false);
            newBoss.SetActive(true);
        }
    }

    IEnumerator BossChanger(GameObject bossDefeated, GameObject newBoss)
    {
        if (!newBoss.active)
        {
            bossDefeated.SetActive(false);
            yield return new WaitForSeconds(2);
            newBoss.SetActive(true);
        }
    }

    public void LifeUpdate(int damage)
    {
        bossDamage += damage * 2;
    }
    #region Level3UI
    void PauseMenu()
    {
        gameplayUI.SetActive(!gameplayUI.activeSelf);
    }
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
    }
    void RefreshHPBar()
    {
        playerFill.fillAmount = player.PlayerHealth / 100f;
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
