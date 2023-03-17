using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    PlayerHud playerHudClass;


    private void Start()
    {
        playerHudClass = FindObjectOfType<PlayerHud>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHudClass.AddCoin();
            
            Destroy(gameObject);
        }
    }


}
