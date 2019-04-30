using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    public List<GameObject> CardUIObjects;
    public GameObject itemTemplate;
    static List<GameObject> items;
    public GameObject content;
    static int itemIndex = 0;
    PersistentGameManager pgm;
    // Start is called before the first frame update
    void Start(){
        pgm = GameObject.Find("PersistentGameManager").GetComponent<PersistentGameManager>();
        items = new List<GameObject>();
        for(int i = 0; i < pgm.playerData.currentDeck.Count; i++)
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
            item.GetComponentInChildren<Text>().text = pgm.playerData.currentDeck[i].cardTitle;

            //Add onClick listener to each item
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Add to Deck: " + (value));
            });

            itemIndex++;
        }
        for (int i = 0; i < 10; i++)
        {
            if(i < pgm.playerData.cardInventory.Count)
            {
                CardUIObjects[i].GetComponent<BasicCard>().SetCardData(pgm.playerData.cardInventory[i]);
                CardUIObjects[i].GetComponent<BasicCard>().SetupUI();

            }
        }
    }

    // Update is called once per frame
    void Update(){}

    public void AddToDeck(int cardID) {
        addItem();
        Debug.Log("Add to Deck: " + cardID);
    }

    private void addItem() {

        int value = itemIndex + 1;

        // Create new item
        var item = Instantiate(itemTemplate);
        items.Add(item);
        item.transform.SetParent(content.transform);

        //Config list item position
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;

        //Add samle text to button's text 
        item.GetComponentInChildren<Text>().text = ("Card " + value).ToString();

        //Add onClick listener to each item
        item.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Add to Deck: " + (value));
        });

        itemIndex++;
    }
}
