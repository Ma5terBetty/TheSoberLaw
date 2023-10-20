using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public bool dontDestroyOnLoad;
    public static EventManager Instance;

    public delegate void OnEnemyKilled();
    public delegate void OnPlayerDead();
    public delegate void OnHpChanged();
    public static event OnEnemyKilled OnKilledEnemy;
    public static event OnPlayerDead OnPlayerDefeat;
    public static event OnHpChanged OnHpVariation;

    AudioSource pauseNoise;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void EnemyKilled()
    {
        OnKilledEnemy();
    }
    public void PlayerDefeated()
    {
        OnPlayerDefeat();
        GameManager.Instance.player.SetActive(false);
    }
    public void HpChanged()
    { 
        OnHpVariation();
    }
}
