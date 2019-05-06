using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackTable : MonoBehaviour
{
    [SerializeField]
    private CardsForPacks[] cardPool;
    //[SerializeField]
    //private CardData[] cardPack;
    public PackTable packTable;
    private List<CardData> playerInv;


    void Start() {
    }


    public List<CardData> rollCardPack() {
        List<CardData> cardPack = new List<CardData>();
        for (int i = 0; i < 5; i++) {
            int index = Random.Range(8, 23);
            cardPack.Add(cardPool[index].Card);
        }
        return cardPack;
    }

    public List<CardData> rollSpecialCardPack() {
        List<CardData> cardPack = new List<CardData>();
        int index = Random.Range(0, 8);
        cardPack.Add(cardPool[index].Card);
        for (int i = 1; i < 5; i++) {
            index = Random.Range(8, 23);
            cardPack.Add(cardPool[index].Card);
        }
        return cardPack;
    }
    public CardData rollCard() {
        CardData card;
       int index = Random.Range(8, 23);
       card = (cardPool[index].Card);
       return card;
    }

    public CardData rollSpecialCard() {
        CardData card;
        int index = Random.Range(0, 8);
        card = (cardPool[index].Card);
        //Debug.Log(card);
        return card;
    }
   
}
