using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : ElementUI
{
    [SerializeField] private Image playerHealthFill;

    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        if (message == ObserverMessages.UpdatePlayerHP)
        {
            playerHealthFill.fillAmount = (float)args[0];
        }
    }
}
