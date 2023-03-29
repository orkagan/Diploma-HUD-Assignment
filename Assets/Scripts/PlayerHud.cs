using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{

    public Image healthBar;
    public Color highHealth;
    public Color lowHealth;
    public Image staminaBarFull;

    public Image restIcon;
    public Image grappleIcon;
    public Image attackIcon;
    public Image tiredIcon;
    public Image playerDamagedIcon;
    public Image playerDeathIcon;

    public Text coinCountText;
    public int coinCount;

    public PlayerStats playerStats; //scriptable object
    PlayerController playerControllerClass;


    private void Start()
    {
        playerControllerClass = GetComponent<PlayerController>();

        restIcon.gameObject.SetActive(true);
        grappleIcon.gameObject.SetActive(false);
        attackIcon.gameObject.SetActive(false);
        tiredIcon.gameObject.SetActive(false);
        playerDamagedIcon.gameObject.SetActive(false);
        playerDeathIcon.gameObject.SetActive(false);
    }

    //Changes player health in to reflect icon changes, as well as reduces healthbar fill amount.
    public void TakeDamage(float damage)
    {
        playerStats.health = Mathf.Clamp( //clamps health so it doesn't go out of range
            playerStats.health - damage,
            0,                          //doesn't go below 0
            playerStats.maxHealth);     //doesn't go above maxhealth
        healthBar.fillAmount = playerStats.health / playerStats.maxHealth;
        healthBar.color = Color.Lerp(lowHealth, highHealth, healthBar.fillAmount);
        PlayerIcon();
    }


    public void AddCoin()
    {
        coinCount++;
        coinCountText.text = (coinCount.ToString());
    }

    public void Heal(int heal)
    {
        TakeDamage(-heal); //undamage lol
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
            GameManager.Instance.GameOver();
        }
    }
    void Update()
    {
        staminaBarFull.fillAmount = playerStats.stamina / playerStats.maxStamina;
        PlayerIconChanger();
    }

    //Changes icon of the player.
    public void PlayerIconChanger()
    {
        //if not taking damage
        if (!DamageHealth._tookDamage)
        {
            //grappling face
            if (playerControllerClass.grappling == true)
            {
                restIcon.gameObject.SetActive(false);
                grappleIcon.gameObject.SetActive(true);
            }
            //out of stamina tired face
            else if (staminaBarFull.fillAmount <= 0)
            {
                restIcon.gameObject.SetActive(false);
                tiredIcon.gameObject.SetActive(true);
            }
            //rest face
            else
            {
                tiredIcon.gameObject.SetActive(false);
                grappleIcon.gameObject.SetActive(false);
                restIcon.gameObject.SetActive(true);
            }
        }
    }

    //Changes Icon and bool to show the player took damage and to give 1 second grace of 'invulnerability'
    public IEnumerator DamageTimer()
    {
        DamageHealth._tookDamage = true;
        playerDamagedIcon.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        playerDamagedIcon.gameObject.SetActive(false);
        DamageHealth._tookDamage = false;
    }

}
