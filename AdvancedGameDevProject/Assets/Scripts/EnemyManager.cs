using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<PlayerData> enemyList;
    public List<GameObject> enemyModels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetEnemybyIndex(int enemyIndex)
    {
        return enemyModels[enemyIndex];
    }
}
