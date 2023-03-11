using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{

    public Image healthBarFull;
    public Image staminaBarFull;
    [SerializeField] private int health = 4;

    public Image fullHealthIcon;
    public Image injuredHealthIcon;
    public Image halfHealthIcon;
    public Image lowHealthIcon;
    public Image deathIcon;

    private float stamina = 10f;

    private void Start()
    {
        fullHealthIcon.gameObject.SetActive(true);
        injuredHealthIcon.gameObject.SetActive(false);
        halfHealthIcon.gameObject.SetActive(false);
        lowHealthIcon.gameObject.SetActive(false);
        deathIcon.gameObject.SetActive(false);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarFull.fillAmount -= 0.25f;
        HealthIcon();
    }

    private void HealthIcon()
    {
        if(health == 3)
        {

            fullHealthIcon.gameObject.SetActive(false);
            injuredHealthIcon.gameObject.SetActive(true);
        }
        if(health == 2)
        {
            injuredHealthIcon.gameObject.SetActive(false);
            halfHealthIcon.gameObject.SetActive(true);

        }
        if(health == 1)
        {
            halfHealthIcon.gameObject.SetActive(false);
            lowHealthIcon.gameObject.SetActive(true);
        }
        if(health == 0)
        {
            lowHealthIcon.gameObject.SetActive(false);
            deathIcon.gameObject.SetActive(true);

            //DisablePlayerControls

        }
    }


}
