using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManagerScript : MonoBehaviour
{

    private List<GameObject> deck;



    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
    //adds a card to the deck
    void addCard(GameObject card) {
        deck.Add(card);
    }
    //draw the top card of the deck
    GameObject drawCard() {
        GameObject card = deck[0];
        deck.RemoveAt(0);
        return card;
    }
    //shuffles the deck by randomizing the order.
    void shuffle() {
        System.Random rng = new System.Random();
        deck = (List<GameObject>)deck.OrderBy(x => rng.Next());
    }

}
