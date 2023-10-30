using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{
    #region Level3UI
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject pauseUI;
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
    public EnemiesOnLevelData ScriptableObj;
    void Start()
    {
        #region Level3UI
        gameplayUI.SetActive(true);
        pauseUI.SetActive(false);
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
        if (initScreen.canvasGroup.alpha > 0) return;
        BossSetter();
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
        if (!newBoss.activeSelf)
        {
            defeatedBoss.SetActive(false);
            newBoss.SetActive(true);
        }
    }

    IEnumerator BossChanger(GameObject bossDefeated, GameObject newBoss)
    {
        if (!newBoss.activeSelf)
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
        pauseUI.SetActive(!pauseUI.activeSelf);
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
        Application.Quit();
    }
    void RefreshHPBar()
    {
        playerFill.fillAmount = player.HealthController.MaxLife / 100f;
    }
    #endregion
}
