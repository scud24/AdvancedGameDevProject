using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasicCard : MonoBehaviour
{
    public Text ManaCostText;
    public Text bonusStatsText;
    public Text AttackPowerText;
    public Text DescriptionText;
    public Image CardImage;

    public int manaCost;
    public int defenceBonus; //if applicable
    public int attackPower;
    public string cardDescription;
    public Sprite cardSprite;

    public GameObject CombatManager;
    public int callBackID;

    // Start is called before the first frame update
    void Start()
    {
        CombatManager = GameObject.Find("CombatManager");
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnCardClick(); });
        trigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set up a card's UI elements after filling its values from the main set of card data
    public void SetupUI()
    {
        ManaCostText.text = manaCost.ToString();
        bonusStatsText.text = defenceBonus.ToString();
        AttackPowerText.text = attackPower.ToString();

        DescriptionText.text = cardDescription;
        CardImage.sprite = cardSprite;
    }

    public void OnCardClick()
    {
        Debug.Log("Click");
        CombatManager.GetComponent<CombatManager>().CardClick(name, tag);
    }
}
