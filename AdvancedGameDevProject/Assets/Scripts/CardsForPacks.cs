using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardsForPacks {
    [SerializeField]
    private CardData card;
    [SerializeField]
    private float dropChance;

    public CardData Card {
        get {
            return card;
        }

    }
    public float DropChance {
        get {
            return dropChance;
        }

    }


}
