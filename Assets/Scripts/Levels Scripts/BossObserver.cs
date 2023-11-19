using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossObserver : MonoBehaviour
{
    [SerializeField] GameObject winnerScreen;
    // Start is called before the first frame update
    public BossDeathEvent OnBossDeath;
    private void Start()
    {
        winnerScreen.SetActive(false);
        GetComponent<Level3Manager>().OnBossDeath += HandleBossDeath;

        if (OnBossDeath == null) OnBossDeath = new BossDeathEvent();
        OnBossDeath.AddListener(HandleBossDeath);

    }

    // Update is called once per frame
    private void HandleBossDeath(object sender, BossDeathEvent e)
    {
        if (e.IsBossDead) winnerScreen.SetActive(true);
    }
    public void NotifyBossDeath(bool isBossDead)
    {
        OnBossDeath.Invoke(isBossDead);
    }
}
