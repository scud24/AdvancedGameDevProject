using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstanceManager : MonoBehaviour
{
    public int enemyIndex;
    public string arenaName;
    public List<string> deactivatedTriggers;
    public int numTriggers;
    public GameObject player;
    public GameObject pgm;
    public GameObject dungeonPrizeMenu;
    static bool created = false;
    public AudioManager am;
    void Awake()
    {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        pgm = GameObject.Find("PersistentGameManager");
        if (!created)
        {
            pgm.GetComponent<PersistentGameManager>().dungeonInProgress = false;
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "environment")
        {

            
            dungeonPrizeMenu = GameObject.Find("WinMenu");
            for(int i = 0; i < deactivatedTriggers.Count; i++)
            {
                GameObject.Find(deactivatedTriggers[i]).SetActive(false);
            }
            player = GameObject.Find("FPSMovePlayer");
            if (pgm.GetComponent<PersistentGameManager>().dungeonInProgress)
            {

                am.PlayMusic(0);
                Debug.Log("player loc start: " + player.transform.position);
                Debug.Log("player last pos: " + pgm.GetComponent<PersistentGameManager>().playerLastDungeonPos);
                pgm.GetComponent<PersistentGameManager>().playerLastDungeonPos.y += 0.1f;
                player.transform.position = pgm.GetComponent<PersistentGameManager>().playerLastDungeonPos;

                Debug.Log("player new pos: " + player.transform.position);
                player.transform.rotation = pgm.GetComponent<PersistentGameManager>().playerLastDungeonOrient;

                //Check if all instances clear
                if (deactivatedTriggers.Count >= numTriggers)
                {
                    dungeonPrizeMenu.GetComponent<DungeonPrizeMenu>().showPrizeMenu();
                }
            }
            else
            {
                Debug.Log("Starting at enterance");
                pgm.GetComponent<PersistentGameManager>().dungeonInProgress = true;
            }

           
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void TriggerInstance(int _enemyIndex, string _arenaName, string triggerName)
    {
        enemyIndex = _enemyIndex;
        if(deactivatedTriggers.Count >= numTriggers -1)
        {
            enemyIndex = 1; //Set enemy to boss for last room
        }
        arenaName = _arenaName;
        deactivatedTriggers.Add(triggerName);
        pgm.GetComponent<PersistentGameManager>().playerLastDungeonPos = player.transform.position;
        pgm.GetComponent<PersistentGameManager>().playerLastDungeonOrient = player.transform.rotation;
        SceneManager.LoadScene(arenaName);
    }
    
}
