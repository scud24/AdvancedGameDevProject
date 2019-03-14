using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManagerScript : MonoBehaviour
{
    public List<GameObject> hand;

    // Start is called before the first frame update
    void Start(){
        hand = new List<GameObject>();
    }

    // Update is called once per frame
    void Update(){
        
    }
    //used with deck.drawcard to add a card to the hand
    public void addCard(GameObject card)
    {
        hand.Add(card);
    }

    //removes a card in the hand at a specific index and returns it.
    public GameObject playCard(int index)
    {
        GameObject card = hand[index];
        hand.RemoveAt(index);
        return card;
    }

    //Returns number of cards in hand currently
    public int Count()
    {
        return hand.Count;
    }

    //Returns a reference to the card at the given index
    public GameObject getCardAtIndex(int index)
    {
        return hand[index];
    }

}
