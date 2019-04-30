using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackOpeningScript : MonoBehaviour
{
    public GameObject pgm;
    public PlayerManagerScript player;
    public DeckManagerScript playerDeck;
    public PackTable packTable;


    void Start()
    {
        pgm = GameObject.Find("PersistantGameManager");
        Debug.Log("Player set");
        player.SetupFromPlayerData(pgm.GetComponent<PersistentGameManager>().playerData);
        Debug.Log("Player deck set");
        playerDeck.SetupFromList(player.currentDeck);


    }

    public void buyPack() {

    }
    public void buyMultiPack() {

    }

    public void buySpecialCard(GameObject card) {

    }

    public void buySpecialPack() {

    }

    public void resetSpecial() {

    }

}
