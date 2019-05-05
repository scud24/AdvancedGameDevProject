using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Dungeon, Inventory, Shop, Save & Quit

    public void Dungeon1() {
        SceneManager.LoadScene(sceneName: "UndergroundDungeon");
    }

    public void Inventory()
    {
        SceneManager.LoadScene(sceneName: "Inventory Menu");
    }

    public void Shop()
    {
        SceneManager.LoadScene(sceneName: "ShopMenu");
    }

    public void SaveQuit()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
