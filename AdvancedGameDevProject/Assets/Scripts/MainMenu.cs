using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<AudioManager>().StopMusic();
        FindObjectOfType<AudioManager>().PlayMusic(0);
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
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
