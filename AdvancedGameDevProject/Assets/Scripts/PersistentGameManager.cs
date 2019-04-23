/**
    ITCS 4231-001 Group Project
    PersistentGameManager.cs
    Purpose: Defines the persistant game manager object that tracks player data between different scenes,
                and handles saving and loading of save files. This file does not control the UI or objects scenes,
                but is referenced by their controllers

    @author Nathan Holzworth, (add your name here if you add stuff to this file)
    @version 1.0 3/8/19
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameManager : MonoBehaviour
{
    //TODO: Add various structures for handling game data here
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        //Todo Add code to check if other instance of this exists and handle accordingly
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SaveGameData()
    {
        //TODO: Add code to save game data
    }

    //Note: might need to change this to a return type other than void.
    void LoadGameData()
    {
        //TODO: Add code to load game data
    }

    //TODO: Add helper functions as needed
}
