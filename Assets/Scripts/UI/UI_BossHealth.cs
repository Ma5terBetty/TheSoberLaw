using UnityEngine;
using UnityEngine.UI;

public class UI_BossHealth : ElementUI
{
    [SerializeField] private Image bossHP;

    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        if (message == ObserverMessages.UpdateBossHP)
        {
            bossHP.fillAmount = (float)args[0];
        }
    }
}
