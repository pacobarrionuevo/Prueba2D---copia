using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneManager : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.transform.position = spawnLocation.position;
        }
    }
}
