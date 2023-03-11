using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{

    public Image healthBarFull;
    public Image staminaBarFull;
    [SerializeField] private int health = 4;

    public Image restIcon;
    public Image grappleIcon;
    public Image attackIcon;
    public Image tiredIcon;
    public Image playerDamagedIcon;
    public Image playerDeathIcon;

    private float stamina = 10f;


    DamageHealth damageHealthClass;


    private void Start()
    {
        damageHealthClass = FindObjectOfType<DamageHealth>();

        restIcon.gameObject.SetActive(true);
        grappleIcon.gameObject.SetActive(false);
        attackIcon.gameObject.SetActive(false);
        tiredIcon.gameObject.SetActive(false);
        playerDamagedIcon.gameObject.SetActive(false);
        playerDeathIcon.gameObject.SetActive(false);
    }

    //Changes player health in to reflect icon changes, as well as reduces healthbar fill amount.
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarFull.fillAmount -= 0.25f;
        PlayerIcon();
    }

    //Changes Player Icon based only on full health and death icons.
    private void PlayerIcon()
    {
        if(health > 0)
        {
            restIcon.gameObject.SetActive(true);
        }
        else if(health == 0)
        {
            restIcon.gameObject.SetActive(false);
            playerDeathIcon.gameObject.SetActive(true);

        }
    }

    //Place setter for now
    /*IEnumerator PlayerIconChanger()
    {

    }*/

    //Changes Icon and bool to show the player took damage and to give 1 second grace of 'invulnerability'
    public IEnumerator DamageTimer()
    {
        damageHealthClass._tookDamage = true;
        playerDamagedIcon.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        playerDamagedIcon.gameObject.SetActive(false);
        damageHealthClass._tookDamage = false;
    }

}
