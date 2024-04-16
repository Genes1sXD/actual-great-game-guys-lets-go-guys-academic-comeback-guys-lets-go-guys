using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float level; //current player level
    [Space]
    public float currentHealth;
    public float maxHealth;

    [Space]
    public float currentXp;
    public float maxXp;
    [Space]
    public float damage;
    public float moveSpeed;

    public Slider healthBar;
    public Slider Xpbar;
    private void Start()
    {

    }
    private void Update()
    {
        maxXp = level + 4 * 2 * 75/100;
}
    public void ChangesSliderUI()
    {
        healthBar.value = currentHealth;
        Xpbar.value = currentXp;

        healthBar.maxValue = maxHealth;
        Xpbar.maxValue = maxXp;

    }
    
}
