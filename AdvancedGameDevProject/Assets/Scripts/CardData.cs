using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public int manaCost;
    public int defenceBonus; //if applicable
    public int attackPower;
    public int numAttacks;
    public string cardTitle;
    public string attackAnimation;
    public Color cardColor;
    // Start is called before the first frame update
    void Start()
    {
        float r = Random.Range(0.1f, 1);
        float g = Random.Range(0.1f, 1);
        float b = Random.Range(0.1f, 1);
        cardColor = new Color(r, g, b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
