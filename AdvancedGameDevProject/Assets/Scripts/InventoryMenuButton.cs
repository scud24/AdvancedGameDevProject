using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuButton : MonoBehaviour
{
    InventoryMenu im;
    private void Start()
    {
        im = GameObject.Find("InventoryMenu").GetComponent<InventoryMenu>();
    }
    InventoryMenu inventoryMenu;
    public void AddToDeck(int cardID)
    {
        im.AddToDeck(cardID);
    }
}
