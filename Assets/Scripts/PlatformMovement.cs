using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float platformSpeed = 4f;
    [SerializeField] private Rigidbody2D platformRb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platformRb.velocity = new Vector2(platformSpeed, platformRb.velocity.y);
        }

        if (collision.CompareTag("MovingPlatform"))
        {
            platformRb.velocity *= -1f;
        }
    }
}
