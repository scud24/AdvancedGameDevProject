using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstanceManager : MonoBehaviour
{
    public int enemyIndex;
    public string arenaName;
    public List<string> deactivatedTriggers;

    static bool created = false;
    void Awake()
    {
        if (!created)
        {
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
            for(int i = 0; i < deactivatedTriggers.Count; i++)
            {
                GameObject.Find(deactivatedTriggers[i]).SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInstanceData(int _enemyIndex, string _arenaName, string triggerName)
    {
        enemyIndex = _enemyIndex;
        arenaName = _arenaName;
        deactivatedTriggers.Add(triggerName);
    }
    public void StartInstance()
    {
        SceneManager.LoadScene(arenaName);
    }
}
