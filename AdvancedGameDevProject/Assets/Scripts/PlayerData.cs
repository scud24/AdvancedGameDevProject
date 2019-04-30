using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<CardData> currrentDeck;
    public List<CardData> cardInventory;
    public int maxHealth;
    public int defence;
    public int defenceBonus;
    public int gold = 10000;
}
