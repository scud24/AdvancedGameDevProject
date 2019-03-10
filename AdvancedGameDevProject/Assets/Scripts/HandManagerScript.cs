using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManagerScript : MonoBehaviour
{
    private List<GameObject> hand;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
    //used with deck.drawcard to add a card to the hand
    void drawCard(GameObject card) {
        hand.Add(card);
    }
    //removes a card in the hand at a specific index and returns it.
    GameObject playCard(int index) {
        GameObject card = hand[index];
        hand.RemoveAt(index);
        return card;
    }

}
