using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHealth : MonoBehaviour
{

    PlayerHud playerHud;
    int damage = 1;

    private void Start()
    {
        playerHud= FindObjectOfType<PlayerHud>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHud.TakeDamage(damage);
        }
    }
}
