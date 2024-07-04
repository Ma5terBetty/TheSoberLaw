using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreCount : ElementUI
{
    [SerializeField] Text scoreAmount;

    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        switch (message)
        {
            case ObserverMessages.EnemyKilled:
                scoreAmount.text = args[0].ToString();
                break;
            case ObserverMessages.LevelStarted:
                scoreAmount.text = args[0].ToString();
                break;
        }
    }
}
