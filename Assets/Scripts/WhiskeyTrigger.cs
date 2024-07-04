using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskeyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var boss = collision.gameObject.GetComponent<Boss>();

        if (collision.gameObject.CompareTag("Boss"))
        {
            boss.TurnOnWhiskey();
        }
    }
}
