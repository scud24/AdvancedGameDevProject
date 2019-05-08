using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    public List<GameObject> CardUIObjects;
    public GameObject itemTemplate;
    static List<GameObject> items;
    public GameObject content;
    static int itemIndex = 0;
    int currentCardPage = 0;
    public GameObject buttonLeft;
    public GameObject buttonRight;
    PersistentGameManager pgm;
    public Text cardsInDeckText;
    // Start is called before the first frame update
    void Start(){
        pgm = GameObject.Find("PersistentGameManager").GetComponent<PersistentGameManager>();

        items = new List<GameObject>();
        for(int i = 0; i < pgm.playerData.currrentDeck.Count; i++)
        {
            int value = itemIndex + 1;
            // Create new item
            var item = Instantiate(itemTemplate);
            items.Add(item);
            item.transform.SetParent(content.transform);

            //Config list item position
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;

            //Add samle text to button's text 
            item.GetComponentInChildren<Text>().text = pgm.playerData.currrentDeck[i].cardTitle;

            item.GetComponent<DeckListButton>().cardIndex = itemIndex;
            

            itemIndex++;
        }
        cardsInDeckText.text = "Cards in Deck: " + items.Count + "/20";


        for (int i = 0; i < 10; i++)
        {
            if (i + (currentCardPage * 10) < pgm.playerData.cardInventory.Count)
            {
                CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(true);
                CardUIObjects[i].GetComponent<BasicCard>().SetCardData(pgm.playerData.cardInventory[i + (currentCardPage * 10)]);
                CardUIObjects[i].GetComponent<BasicCard>().SetupUI();

            }
            else
            {
                CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update(){}

    public void AddToDeck(int cardID) {
        addItem(cardID);
        Debug.Log("Add to Deck from slot: " + cardID + " (index " + (cardID-1) + ")");
    }

    private void addItem(int cardID) {
        if (items.Count < 20)
        {
            cardID = cardID - 1 + (10*currentCardPage); //Adjust cardID to give index, not slot number
            int value = itemIndex + 1;

            // Create new item
            var item = Instantiate(itemTemplate);
            items.Add(item);
            item.transform.SetParent(content.transform);

            //Config list item position
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;


            //Add samle text to button's text 
            item.GetComponentInChildren<Text>().text = pgm.playerData.cardInventory[cardID].cardTitle;
            pgm.playerData.currrentDeck.Add(pgm.playerData.cardInventory[cardID]);
            pgm.playerData.cardInventory.RemoveAt(cardID);

            UpdateInventoryListUI();
            item.GetComponent<DeckListButton>().cardIndex = itemIndex;
            itemIndex++;
        }
        else
        {
            Debug.Log("Inventory full");
        }
        cardsInDeckText.text = "Cards in Deck: " + items.Count + "/20";
    }

    //Navigates to previous page
    public void DeckLeftButton()
    {
        if (0 <= currentCardPage - 1)
        {
         
            currentCardPage -= 1;
            for (int i = 0; i < 10; i++)
            {
                if (i + (currentCardPage * 10) < pgm.playerData.cardInventory.Count)
                {
                    CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(true);
                    CardUIObjects[i].GetComponent<BasicCard>().SetCardData(pgm.playerData.cardInventory[i + (currentCardPage * 10)]);
                    CardUIObjects[i].GetComponent<BasicCard>().SetupUI();

                }
                else
                {
                   CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(false);
                }
            }
        }
        else //Error case- should lock before
        {
            buttonLeft.GetComponent<Button>().interactable = false;
        }

        if (0 == currentCardPage)
        {
            buttonLeft.GetComponent<Button>().interactable = false;
        }

        if (((int)(pgm.playerData.cardInventory.Count / 10)) >= currentCardPage + 1)
        {
            buttonRight.GetComponent<Button>().interactable = true;
        }
    }


    //Navigates to next page
    public void DeckRightButton()
    {
        if (((int)(pgm.playerData.cardInventory.Count / 10)) >= currentCardPage)
        {
            currentCardPage += 1;
            for (int i = 0; i < 10; i++)
            {
                if (i + (currentCardPage * 10) < pgm.playerData.cardInventory.Count)
                {
                    CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(true);
                    CardUIObjects[i].GetComponent<BasicCard>().SetCardData(pgm.playerData.cardInventory[i + (currentCardPage * 10)]);
                    CardUIObjects[i].GetComponent<BasicCard>().SetupUI();

                }
                else
                {
                    CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(false);
                }
            }
        }
        else //Error case- should lock before
        {
            buttonRight.GetComponent<Button>().interactable = false;
        }

        if ((int)(pgm.playerData.cardInventory.Count / 10) == currentCardPage)
        {
            buttonRight.GetComponent<Button>().interactable = false;
        }

        if (0 <= currentCardPage - 1)
        {
            buttonLeft.GetComponent<Button>().interactable = true;
        }
    }
    public void UpdateDeckListUI()
    {
        Debug.Log("UpdateUI");

        cardsInDeckText.text = "Cards in Deck: " + items.Count + "/20";
        string output = "";
        for (int i = 0; i < pgm.playerData.currrentDeck.Count; i++)
        {
            output = output + pgm.playerData.currrentDeck[i].cardTitle + ", ";
        }
        Debug.Log(output);
        for (int i = items.Count-1; i >=0; i--)
        {
            Destroy(items[i].gameObject);
            items.RemoveAt(i);
        }
        itemIndex = 0;
        for (int i = 0; i < pgm.playerData.currrentDeck.Count; i++)
        {
            int value = itemIndex + 1;
            // Create new item
            var item = Instantiate(itemTemplate);
            items.Add(item);
            item.transform.SetParent(content.transform);

            //Config list item position
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;

            //Add samle text to button's text 
            item.GetComponentInChildren<Text>().text = pgm.playerData.currrentDeck[i].cardTitle;

            /*//Add onClick listener to each item
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Remove from Deck: " + (value));
                pgm.playerData.currrentDeck.RemoveAt(value);
                UpdateDeckListUI();
                Destroy(items[value].GetComponent<Button>().gameObject);
            });*/

            item.GetComponent<DeckListButton>().cardIndex = itemIndex;
            itemIndex++;
        }
    }
    public void UpdateInventoryListUI()
    {
            for (int i = 0; i < 10; i++)
            {
                if (i + (currentCardPage * 10) < pgm.playerData.cardInventory.Count)
                {
                    CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(true);
                    CardUIObjects[i].GetComponent<BasicCard>().SetCardData(pgm.playerData.cardInventory[i + (currentCardPage * 10)]);
                    CardUIObjects[i].GetComponent<BasicCard>().SetupUI();

                }
                else
                {
                    CardUIObjects[i].GetComponent<BasicCard>().gameObject.SetActive(false);
                }
            }
    }

    public void BackToMapButton()
    {
        SceneManager.LoadScene("Map Menu");
    }
}
