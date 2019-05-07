using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListButton : MonoBehaviour
{
    public int cardIndex;
    public PersistentGameManager pgm;
    public InventoryMenu iMenu;
    // Start is called before the first frame update
    void Start()
    {
        pgm = GameObject.Find("PersistentGameManager").GetComponent<PersistentGameManager>();
        iMenu = GameObject.FindObjectOfType<InventoryMenu>();
        //Add onClick listener to each item
        name = "Index " + cardIndex + " button";
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log(name +": Remove from Deck at index: " + (cardIndex) + " title: " + GetComponentInChildren<Text>().text);
            pgm.playerData.cardInventory.Add(pgm.playerData.currrentDeck[cardIndex]);
            pgm.playerData.currrentDeck.RemoveAt(cardIndex);

            iMenu.UpdateDeckListUI();
            iMenu.UpdateInventoryListUI();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
