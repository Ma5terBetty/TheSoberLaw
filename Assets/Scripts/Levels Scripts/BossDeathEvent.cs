using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class BossDeathEvent : UnityEvent<bool>
{
    public bool IsBossDead { get; }
    public BossDeathEvent(bool isBossDead)
    {
        IsBossDead = isBossDead;
    }

}
