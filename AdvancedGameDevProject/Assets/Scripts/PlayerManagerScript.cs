﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour
{
    public List<CardData> currentDeck;
    public int currentHealth;
    public int maxHealth;
    public int defence;
    public int defenceBonus;
    public bool isDead;

    private void Start()
    {
        isDead = false;
    }

    public void DamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool CheckIfDead()
    {
        return isDead;
    }

    public void SetupFromPlayerData(PlayerData pd)
    {
        maxHealth = pd.maxHealth;
        currentHealth = maxHealth;
        defence = pd.defence;
        defenceBonus = pd.defenceBonus;
        currentDeck = pd.currrentDeck;
    }
}
