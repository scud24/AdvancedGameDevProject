using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManagerScript : MonoBehaviour
{

    public List<CardData> deck;



    // Start is called before the first frame update
    void Start(){
        if(deck==null)
        {
            deck = new List<CardData>();
        }
    }


    public void SetupFromList(List<CardData> cdl)
    {
        deck = new List<CardData>();
        for (int i = 0; i < cdl.Count; i++)
        {
            deck.Add(cdl[i]);
        }
    }

    //adds a card to the deck
    public void addCard(CardData card) {
        deck.Add(card);
    }

    //draw the top card of the deck
    public CardData drawCard() {
        CardData card = deck[0];
        //Debug.Log(card);
        deck.RemoveAt(0);
        //Debug.Log(card);
        return card;
    }
    //shuffles the deck by randomizing the order.
    public void shuffle() {
        System.Random rng = new System.Random();
        deck = deck.OrderBy(x => rng.Next()).ToList<CardData>();
    }


    //Returns number of cards in hand currently
    public int Count()
    {
        return deck.Count;
    }

}
