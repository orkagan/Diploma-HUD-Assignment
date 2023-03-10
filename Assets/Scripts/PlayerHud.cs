using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{

    public Image healthBarFull;
    public Image staminaBarFull;

    public Image restIcon;
    public Image grappleIcon;
    public Image attackIcon;
    public Image tiredIcon;
    public Image playerDamagedIcon;
    public Image playerDeathIcon;

    public PlayerStats playerStats; //scriptable object

    DamageHealth damageHealthClass;
    PlayerController playerControllerClass;


    private void Start()
    {
        damageHealthClass = FindObjectOfType<DamageHealth>();
        playerControllerClass = GetComponent<PlayerController>();

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
        playerStats.health -= damage;
        healthBarFull.fillAmount = playerStats.health / playerStats.maxHealth;
        PlayerIcon();
    }

    //Changes Player Icon based only on full health and death icons.
    private void PlayerIcon()
    {
        if (playerStats.health > 0)
        {
            restIcon.gameObject.SetActive(true);
        }
        else if (playerStats.health == 0)
        {
            restIcon.gameObject.SetActive(false);
            playerDeathIcon.gameObject.SetActive(true);

        }
    }
    void Update()
    {
        staminaBarFull.fillAmount = playerStats.stamina / playerStats.maxStamina;
    }

    //Changes icon of the player.
    public IEnumerator PlayerIconChanger()
    {
        if (playerControllerClass.grappling == true && staminaBarFull.fillAmount >0)
        {
            restIcon.gameObject.SetActive(false);
            grappleIcon.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            playerControllerClass.grappling = false;
            grappleIcon.gameObject.SetActive(false);
            restIcon.gameObject.SetActive(true);

        }
        if(staminaBarFull.fillAmount <= 0)
        {
            restIcon.gameObject.SetActive(false);
            tiredIcon.gameObject.SetActive(true);
        }
    }

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
