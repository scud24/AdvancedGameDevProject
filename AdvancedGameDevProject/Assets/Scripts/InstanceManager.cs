using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    public int enemyIndex;
    public string arenaName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInstanceData(int _enemyIndex, string _arenaName)
    {
        enemyIndex = _enemyIndex;
        arenaName = _arenaName;
    }
}
