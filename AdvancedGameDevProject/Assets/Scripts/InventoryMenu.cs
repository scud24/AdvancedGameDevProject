using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    public GameObject itemTemplate;
    static List<GameObject> items;
    public GameObject content;
    static int itemIndex = 0;

    // Start is called before the first frame update
    void Start(){
        items = new List<GameObject>(); 
    }

    // Update is called once per frame
    void Update(){}

    public void AddToDeck(int cardID) {
        this.addItem();
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
