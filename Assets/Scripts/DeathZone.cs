using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    PlayerHud playerHudClass;
    public PlayerStats playerStats;
    public static bool _tookDamage;

    private void Start()
    {
        playerHudClass = FindObjectOfType<PlayerHud>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_tookDamage)
        {
            playerHudClass.TakeDamage(playerStats.maxHealth);
            StartCoroutine(playerHudClass.DamageTimer());
        }
    }
}
