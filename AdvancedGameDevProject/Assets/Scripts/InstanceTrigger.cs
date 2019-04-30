using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceTrigger : MonoBehaviour
{
    public int enemyIndex;
    public string arenaName;
    public InstanceManager im;
    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.Find("InstanceManager").GetComponent<InstanceManager>();
        arenaName = "CombatScene";
        enemyIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ping: " + other.tag);
        if(other.tag == "Player")
        {
            im.SetInstanceData(enemyIndex, arenaName, name);
            im.StartInstance();
        }
    }
}
