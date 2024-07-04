using UnityEngine;
using UnityEngine.UI;

public class UI_VictoryScreen : ElementUI
{
    [SerializeField] private Image background;
    [SerializeField] private Text title;
    [SerializeField] private Text finalMessage;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitGameButton;
    private void Start()
    {
        HideItems();    
    }
    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        if (message == ObserverMessages.GameWon)
        { 
            ShowItems();
        }
    }

    private void ShowItems()
    {
        background.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        exitGameButton.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        finalMessage.gameObject.SetActive(true);
    }

    private void HideItems()
    { 
        background.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        exitGameButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        finalMessage.gameObject.SetActive(false);
    }
}
