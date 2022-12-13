using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    //THIS SCRIPT AVOID THAT PLAYER PUSH NPCS WHEN COLLIDE
    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("NPC"))
        {
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("NPC") || other.collider.CompareTag("Player"))
        {
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
        }

    }
}

