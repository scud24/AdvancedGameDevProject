using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour {
    public GameObject pgm;
    public PackTable packTable;
    private List<CardData> playerInv;

    public GameObject SpecialCard1;
    public GameObject SpecialCard2;
    public GameObject SpecialCard3;
    public List<BasicCard> PackCardsUI;
    public GameObject CardPackDisplayPanel;
    public Text PlayerGoldLabel;

    void Start() {
        pgm = GameObject.Find("PersistentGameManager");
        playerInv = pgm.GetComponent<PersistentGameManager>().playerData.cardInventory;
        resetSpecialCards(); //should move this to PersistentGameManager to only activate on first load and on dungeon clears

    }

    // Update is called once per frame
    void Update() {
        PlayerGoldLabel.text = "Gold: " + pgm.GetComponent<PersistentGameManager>().playerData.gold.ToString();
    }
    public void addPackToDeck(List<CardData> cardPack) {

        for (int i = 0; i < cardPack.Count; i++) {
            playerInv.Add(cardPack[i]);
        }
    }

    public void buyPack() {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 50) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 50;
            List<CardData> pack = packTable.rollCardPack();
            addPackToDeck(pack);
            DisplayCardPack(pack);
        }
        else {
            Debug.Log("Not enough gold");
        }

    }
    public void buyMultiPack() {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 150) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 150;
            for (int i = 0; i < 3; i++) {
                List<CardData> pack = packTable.rollCardPack();
                addPackToDeck(pack);
            }
        }
        else {
            Debug.Log("Not enough gold");
        }

    }

    public void buySpecialCard(BasicCard card) {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 50) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 50;
            playerInv.Add(card.cardData);
            List<CardData> pack = new List<CardData>();
            pack.Add(card.cardData);
            DisplayCardPack(pack);
            card.GetComponentInChildren<Button>().interactable = false;
        }
        else {
            Debug.Log("Not enough gold");
        }

    }

    public void buySpecialPack() {
        if (pgm.GetComponent<PersistentGameManager>().playerData.gold >= 50) {
            pgm.GetComponent<PersistentGameManager>().playerData.gold -= 50;
            List<CardData> pack = packTable.rollSpecialCardPack();
            addPackToDeck(pack);
            DisplayCardPack(pack);
        }
        else {
            Debug.Log("Not enough gold");
        }


    }
    public void resetSpecialCards() {
        SpecialCard1.GetComponent<BasicCard>().SetCardData(packTable.rollSpecialCard());
        SpecialCard1.GetComponent<BasicCard>().SetupUI();
        SpecialCard1.SetActive(true);
        SpecialCard1.GetComponentInChildren<Button>().interactable = false;

        SpecialCard2.GetComponent<BasicCard>().SetCardData(packTable.rollSpecialCard());
        SpecialCard2.GetComponent<BasicCard>().SetupUI();
        SpecialCard2.SetActive(true);
        SpecialCard1.GetComponentInChildren<Button>().interactable = true;

        SpecialCard3.GetComponent<BasicCard>().SetCardData(packTable.rollSpecialCard());
        SpecialCard3.GetComponent<BasicCard>().SetupUI();
        SpecialCard3.SetActive(true);
        SpecialCard1.GetComponentInChildren<Button>().interactable = true;

    }

    public void exitToMenu() {
        SceneManager.LoadScene(sceneName: "Map Menu");

    }

    public void DisplayCardPack(List<CardData> pack)
    {
        int max = 5; //UI only currently set up to show at most 5 cards atm
        if(pack.Count < 5)
        {
            max = pack.Count;
        }
        for(int i = 0; i < max; i++)
        {
            PackCardsUI[i].SetCardData(pack[i]);
            PackCardsUI[i].SetupUI();
            PackCardsUI[i].gameObject.SetActive(true);
        }
        CardPackDisplayPanel.SetActive(true);
    }

    public void HideCardPackPanel()
    {
        int max = 5; //UI only currently set up to show at most 5 cards atm
        
        for (int i = 0; i < max; i++)
        {
            PackCardsUI[i].gameObject.SetActive(false);
        }
        CardPackDisplayPanel.SetActive(false);
    }




}
