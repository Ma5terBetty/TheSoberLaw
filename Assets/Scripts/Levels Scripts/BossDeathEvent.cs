using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathEvent : EventArgs
{
    public bool IsBossDead { get; }
    public BossDeathEvent(bool isBossDead)
    {
        IsBossDead = isBossDead;
    }

}
