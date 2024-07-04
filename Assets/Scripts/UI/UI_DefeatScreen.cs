using UnityEngine;
using UnityEngine.UI;

public class UI_DefeatScreen : ElementUI
{
    [SerializeField] private Image background;
    [SerializeField] private Text gameOverTitle;
    [SerializeField] private GameObject buttonsContainer;

    void Start()
    {
        HideItems();
    }

    private void ShowItems()
    {
        background.enabled = true;
        buttonsContainer.SetActive(true);
        gameOverTitle.enabled = true;
    }

    private void HideItems()
    {
        background.enabled = false;
        buttonsContainer.SetActive(false);
        gameOverTitle.enabled = false;
    }

    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        if (message == ObserverMessages.GameOver)
        {
            ShowItems();
        }
    }
}
