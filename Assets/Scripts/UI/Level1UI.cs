using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1UI : MonoBehaviour
{
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] Text counter;
    [SerializeField] Player player;
    public Image hpFill;

    void Start()
    {
        defeatScreen = transform.GetChild(2).gameObject;

        gameplayUI.SetActive(true);
        pauseUI.SetActive(false);
        defeatScreen.SetActive(false);
        EventManager.OnPlayerDefeat += DefeatScreen;
        EventManager.OnHpVariation += RefreshHPBar;
    }

    void PauseMenu()
    {
        if (gameplayUI == null) return;
        gameplayUI.SetActive(!gameplayUI.activeSelf);
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    void Update()
    {
        if (GameManager.IsGamePaused)
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

    void DefeatScreen()
    {
        if (defeatScreen == null) return;

        defeatScreen.SetActive(true);
        EventManager.OnPlayerDefeat -= DefeatScreen;
        EventManager.OnHpVariation -= RefreshHPBar;
    }

    public void MainMenu()
    {
        //EventManager.Instance.Pause();
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
        hpFill.fillAmount = player.HealthController.MaxLife / 100f;
    }
}
