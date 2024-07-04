using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : ElementUI
{
    [SerializeField] private Image background;
    [SerializeField] private Text pauseTitle;
    [SerializeField] private GameObject buttonsContainer;

    void Start()
    {
        HideItems();
    }

    private void ShowItems()
    { 
        background.enabled = true;
        buttonsContainer.SetActive(true);
        pauseTitle.enabled = true;
    }

    private void HideItems()
    {
        background.enabled = false;
        buttonsContainer.SetActive(false);
        pauseTitle.enabled = false;
    }

    public void MainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void ExitGame()
    { 
        GameManager.Instance.ExitGame();
    }

    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        if(message == ObserverMessages.GamePaused)
        {
            if (GameManager.IsGamePaused)
            {
                ShowItems();
            }
            else
            {
                HideItems();
            }
        }
    }
}
