using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicCard : MonoBehaviour
{
    public Text ManaCostText;
    public Text HealthText;
    public Text AttackPowerText;
    public Text DescriptionText;
    public Image CardImage;

    public int manaCost;
    public int health;
    public int attackPower;
    public string cardDescription;
    public Sprite cardSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set up a card's UI elements after filling its values from the main set of card data
    public void SetupUI()
    {
        ManaCostText.text = manaCost.ToString();
        HealthText.text = health.ToString();
        AttackPowerText.text = attackPower.ToString();

        DescriptionText.text = cardDescription;
        CardImage.sprite = cardSprite;
    }
}
