using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2UI : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] Text timer;
    [SerializeField] Level2Manager lvlManager;
    [SerializeField] Player player;
    [SerializeField] Image playerFill;

    private void Awake()
    {
        defeatScreen = transform.GetChild(2).gameObject;
    }
    void Start()
    {
        defeatScreen = transform.GetChild(2).gameObject;
        timer.text = "30";
        gameplayUI.SetActive(true);
        pauseUI.SetActive(false);
        defeatScreen.SetActive(false);
        EventManager.OnPlayerDefeat += DefeatScreen;
        EventManager.OnHpVariation += RefreshHPBar;
    }

    private void Update()
    {
        if (!GameManager.isGamePaused)
        {
            timer.text = Mathf.RoundToInt(lvlManager.counter).ToString();
            gameplayUI.SetActive(true);
            pauseUI.SetActive(false);
        }
        else
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(true);
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
