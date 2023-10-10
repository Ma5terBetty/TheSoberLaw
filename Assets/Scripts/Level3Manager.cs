using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{
    public GameObject[] bosses;

    public Transform[] PatrolPoints;

    public GameObject particles;

    ParticleSystem particlesPrefab;

    public InitialScreen initScreen;

    bool isParticleUsed;

    public int bossDamage;

    public int currentDestination;

    public bool isReturning;

    int currentBossStage;

    void Start()
    {
        bossDamage = 0;
        isReturning = false;
        currentDestination = 3;

        GameManager.Instance.isBossDefeated = false;

        particlesPrefab = particles.GetComponent<ParticleSystem>();

        for (int i = 0; i < bosses.Length; i++)
        {
            bosses[i].SetActive(false);
        }
    }
    void Update()
    {
        if (initScreen.canvasGroup.alpha > 0) return;

        BossSetter();
    }

    void BossSetter()
    {
        if (bossDamage >= 100)
        {
            bosses[2].SetActive(false);
            GameManager.Instance.isBossDefeated |= true;
            return;
        }

        if (bossDamage >= 75)
        {
            StartCoroutine(BossChanger(bosses[1], bosses[2]));
            return;
        }
        
        if (bossDamage >= 50)
        {
            StartCoroutine(BossChanger(bosses[0], bosses[1]));
            return;
        }

        if (bossDamage < 50)
        {
            bosses[0].SetActive(true);
        }
    }

    void ChangeBoss(GameObject defeatedBoss, GameObject newBoss)
    {
        if (!newBoss.active)
        {
            defeatedBoss.SetActive(false);
            newBoss.SetActive(true);
        }
    }

    IEnumerator BossChanger(GameObject bossDefeated, GameObject newBoss)
    {
        if (!newBoss.active)
        {
            bossDefeated.SetActive(false);
            yield return new WaitForSeconds(2);
            newBoss.SetActive(true);
        }
    }

    public void LifeUpdate(int damage)
    {
        bossDamage += damage * 2;
    }
}
