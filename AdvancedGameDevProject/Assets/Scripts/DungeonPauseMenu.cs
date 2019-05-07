using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
        }
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Map Menu");
    }
    public void CancelButton()
    {
        pauseMenuUI.SetActive(false);
    }
}
