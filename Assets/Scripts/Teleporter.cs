using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var boss = collision.gameObject.GetComponent<Boss>();

        if (collision.gameObject.CompareTag("Boss"))
        {
            boss.transform.position = transform.position;
            boss.Turn();
        }
    }
}
