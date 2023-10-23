using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public float timer = 0;
    //public Text textoTimer;
    public TextMeshProUGUI textoTimerPro;

    void Update()
    {
        timer -= Time.deltaTime;
        //textoTimer.text = "" + timer.ToString("f0");
        textoTimerPro.text = "" + timer.ToString("f0");
        if (timer <= 0)
        {
            EventManager.Instance.PlayerDefeated();
            GameManager.Instance.gameOver = true;
        }
    }


}
