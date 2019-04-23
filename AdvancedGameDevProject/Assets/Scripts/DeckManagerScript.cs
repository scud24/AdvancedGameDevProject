using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManagerScript : MonoBehaviour
{

    public List<GameObject> deck;



    // Start is called before the first frame update
    void Start(){
        if(deck==null)
        {
            deck = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
    //adds a card to the deck
    public void addCard(GameObject card) {
        deck.Add(card);
    }
    //draw the top card of the deck
    public GameObject drawCard() {
        GameObject card = deck[0].gameObject;
        //Debug.Log(card);
        deck.RemoveAt(0);
        //Debug.Log(card);
        return card;
    }
    //shuffles the deck by randomizing the order.
    public void shuffle() {
        System.Random rng = new System.Random();
        deck = deck.OrderBy(x => rng.Next()).ToList<GameObject>();
    }


    //Returns number of cards in hand currently
    public int Count()
    {
        return deck.Count;
    }

}
