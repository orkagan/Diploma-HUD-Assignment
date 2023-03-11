using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHealth : MonoBehaviour
{

    PlayerHud playerHudClass;
    public int damage = 1;
    public bool _tookDamage;

    private void Start()
    {
        playerHudClass= FindObjectOfType<PlayerHud>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_tookDamage)
        {
            playerHudClass.TakeDamage(damage);
            StartCoroutine(playerHudClass.DamageTimer());
        }
        
    }
}
