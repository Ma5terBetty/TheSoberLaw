using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3UI : MonoBehaviour
{
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject winnerScreen;
    [SerializeField] Level3Manager lvlManager;
    [SerializeField] Player player;
    [SerializeField] Image bossLife;
    [SerializeField] Image playerFill;
    void Start()
    {
        gameplayUI.SetActive(true);
        pauseUI.SetActive(false);
        defeatScreen.SetActive(false);
        winnerScreen.SetActive(false);

        bossLife.fillAmount = 1f;

        EventManager.OnPlayerDefeat += DefeatScreen;
        EventManager.OnHpVariation += RefreshHPBar;
    }

    private void Update()
    {
        if (GameManager.Instance.isBossDefeated)
        {
            winnerScreen.SetActive(true);
        }

        bossLife.fillAmount = 1 - lvlManager.bossDamage / 100f;

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
    }
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
}
