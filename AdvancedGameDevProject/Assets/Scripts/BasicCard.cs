using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasicCard : MonoBehaviour
{
    public Text bonusStatsText;
    public Text AttackPowerText;
    public Text TitleText;
    public Image CardImage;


    public CardData cardData;
    public Sprite cardSprite;

    public GameObject CombatManager;
    public int callBackID;


    public bool sendsClick = false;
    // Start is called before the first frame update
    void Start()
    {
        CombatManager = GameObject.Find("CombatManager");
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnCardClick(); });
        trigger.triggers.Add(entry);

        
        //SetupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set up a card's UI elements after filling its values from the main set of card data
    public void SetupUI()
    {
        bonusStatsText.text = cardData.defenceBonus.ToString();
        AttackPowerText.text = cardData.attackPower.ToString();

        TitleText.text = cardData.cardTitle;
        //CardImage.sprite = cardSprite;
        CardImage.color = cardData.cardColor;
    }
    public void SetCardData(CardData cd)
    {
        cardData = cd;
    }

    public void OnCardClick()
    {
        if (sendsClick)
        {
            Debug.Log("Click from " + name);
            CombatManager.GetComponent<CombatManager>().CardClick(name, tag);
        }
    }
}
