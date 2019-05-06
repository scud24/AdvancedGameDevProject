using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame(string data) {
        Debug.Log("New Game Button Clicked");
        SceneManager.LoadScene("Map Menu");
    }

    public void LoadGame(string data)
    {
        Debug.Log("Load Game Button Clicked");
        SceneManager.LoadScene("Map Menu");
    }

    public void Controls(string data)
    {
        Debug.Log("Controlls Button Clicked");
    }

    public void QuitGame(string data)
    {
        Debug.Log("Quit Game Button Clicked");
        Application.Quit();
    }
}
