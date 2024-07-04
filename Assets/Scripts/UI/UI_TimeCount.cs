using UnityEngine;
using UnityEngine.UI;

public class UI_TimeCount : ElementUI
{
    [SerializeField] Text counter;

    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        switch (message)
        {
         
            case ObserverMessages.UpdateTimeCount:
                counter.text = args[0].ToString();
                break;
            case ObserverMessages.LevelStarted:
                counter.text = args[0].ToString();
                break;
        }
    }
}
