using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public float timer = 0;
    public TextMeshProUGUI textoTimerPro;

    void Update()
    {
        timer -= Time.deltaTime;
        textoTimerPro.text = "" + timer.ToString("f0");
        if (timer <= 0)
        {
            EventManager.Instance.PlayerDefeated();
            GameManager.Instance.OnNotify(ObserverMessages.GameOver);
        }
    }


}
