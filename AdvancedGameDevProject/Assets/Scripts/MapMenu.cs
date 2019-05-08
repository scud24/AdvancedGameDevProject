using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        //FindObjectOfType<AudioManager>().StopSoundbyName("Combat");
        FindObjectOfType<AudioManager>().PlayMusic(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Dungeon, Inventory, Shop, Save & Quit

    public void Dungeon(string a) {
        Debug.Log(a);
        SceneManager.LoadScene("environment");
    }

    public void Inventory(string a)
    {
        Debug.Log(a);
        SceneManager.LoadScene("menu");
    }

    public void Shop(string a)
    {
        Debug.Log(a);
        SceneManager.LoadScene("ShopMenu");
    }

    public void SaveQuit(string a)
    {
        Debug.Log(a);
        SceneManager.LoadScene("MainMenu");
    }
}
