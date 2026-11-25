using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneManager : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;
    [SerializeField, Min(1f)] private float damage = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.transform.position = spawnLocation.position;

            Character target = collision.GetComponentInParent<Character>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
