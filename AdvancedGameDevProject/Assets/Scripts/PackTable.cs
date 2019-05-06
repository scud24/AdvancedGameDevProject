using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackTable : MonoBehaviour
{
    [SerializeField]
    private CardsForPacks[] cardPool;
    //[SerializeField]
    //private CardData[] cardPack;
    public GameObject pgm;
    public PackTable packTable;
    private List<CardData> playerInv;


    void Start() {
        pgm = GameObject.Find("PersistantGameManager");
        playerInv = pgm.GetComponent<PersistentGameManager>().playerData.cardInventory;
    }


    public CardData[] rollCardPack() {
        CardData[] cardPack = new CardData[5];
        for (int i = 0; i < 5; i++) {
            int index = Random.Range(8, 23);
            cardPack[i] = (cardPool[index].Card);
        }
        return cardPack;
    }

    public CardData[] rollSpecialCardPack() {
        CardData[] cardPack = new CardData[5];
        int index = Random.Range(0, 8);
        cardPack[0] = (cardPool[index].Card);
        for (int i = 1; i < 5; i++) {
            index = Random.Range(8, 23);
            cardPack[i] = (cardPool[index].Card);
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
        Debug.Log(card);
        return card;
    }
    /*
    public void addPackToDeck(CardData[] cardPack) {
        
        for (int i = 0; i < 5; i++) {
            playerInv.Add(cardPack[i]);
        }
    }
    
    public void buyPack() {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 50) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 50;
            CardData[] pack = rollCardPack();
            addPackToDeck(pack);
        }
        else {
            Debug.Log("Not enough gold");
        }
        
    }
    public void buyMultiPack() {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 150) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 150;
            for (int i = 0; i < 3; i++) {
                CardData[] pack = rollCardPack();
                addPackToDeck(pack);
            }
        }
        else {
            Debug.Log("Not enough gold");
        }

    }

    public void buySpecialCard(CardData card) {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 50) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 50;
            playerInv.Add(card);
        }
        else {
            Debug.Log("Not enough gold");
        }

    }

    public void buySpecialPack() {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 50) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 50;
            CardData[] pack = rollSpecialCardPack();
            addPackToDeck(pack);
        }
        else {
            Debug.Log("Not enough gold");
        }
    }


    */

}
