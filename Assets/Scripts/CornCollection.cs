using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornCollection : MonoBehaviour
{
    PlayerHud playerHudClass;

    public PlayerStats playerStats;

    //int heal = 1;

    private void Start()
    {
        playerHudClass= FindObjectOfType<PlayerHud>();
    }

    //Need to redo this for stamina
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerStats.health < 4)
        {
            playerHudClass.Heal(heal);

            Destroy(gameObject);
        }
    }*/
}
