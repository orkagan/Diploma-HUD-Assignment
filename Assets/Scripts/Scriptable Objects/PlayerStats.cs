using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "ScriptableObjects/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float health = 4f;
    public float maxHealth = 4f;
    public float stamina = 100f;
    public float maxStamina = 100f;
}
