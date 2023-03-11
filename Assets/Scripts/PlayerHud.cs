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


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarFull.fillAmount -= 0.25f;
        PlayerIcon();
    }

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

    /*IEnumerator PlayerIconChanger()
    {

    }*/

    public IEnumerator DamageTimer()
    {
        damageHealthClass._tookDamage = true;
        playerDamagedIcon.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        playerDamagedIcon.gameObject.SetActive(false);
        damageHealthClass._tookDamage = false;
    }

}
