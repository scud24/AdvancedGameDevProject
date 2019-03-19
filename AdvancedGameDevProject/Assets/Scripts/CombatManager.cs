using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatManager : MonoBehaviour
{
    public PlayerManagerScript player;
    public DeckManagerScript playerDeck;
    //public HandManagerScript playerHand;
    //public Vector3 playerHandPos;
    //public Vector3 playerSelectedCardPos;
    //public Vector3 playerDiscardPos;
    //public float playerHandOffset;
    //public float playerHandWidth;
    public GameObject playerCurrentCard;
    public GameObject playerCurrentCardUI;
    public List<GameObject> playerDiscard;
    public GameObject playerDiscardUI;
    public GameObject playerHandUI1;
    public GameObject playerHandUI2;
    public GameObject playerHandUI3;

    public PlayerManagerScript enemy;
    public DeckManagerScript enemyDeck;
    public HandManagerScript enemyHand;
    //public Vector3 enemyHandPos;
    //public Vector3 enemySelectedCardPos;
    //public Vector3 enemyDiscardPos;
    //public float enemyHandOffset;
    //public float enemyHandWidth;
    public GameObject enemyCurrentCard;
    public GameObject enemyCurrentCardUI;
    public List<GameObject> enemyDiscard;
    public GameObject enemyDiscardUI;

    public List<GameObject> dummyCards;
    public float MAX_TURN_END_WAIT;
    public float currentTurnEndWait;
    public bool waitingForNextTurn;
    public GameObject playArea;
    public GameObject endPanel;
    public GameObject endPanelText;
    public GameObject startButton;
    public GameObject playerTurnButton;

    public Animator playerAnimator;
    public Animator enemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < dummyCards.Count; i++)
        {
            playerDeck.addCard(dummyCards[i]);
            enemyDeck.addCard(dummyCards[i]);
        }
        Debug.Log(playerDeck.Count());
        Debug.Log(enemyDeck.Count());
        enemy.health = 10;
        player.health = 10;

        playerDeck.shuffle();
        enemyDeck.shuffle();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentTurnEndWait > 0)
        {
            currentTurnEndWait--;
        }
        else if(waitingForNextTurn)
        {
            waitingForNextTurn = false;
            combatResolution();
        }
    }

    public void PlayerTurn()
    {
        if (playerDeck.Count() > 0)
        {
            GameObject newCardData = playerDeck.drawCard();
            playerHandUI1.GetComponent<BasicCard>().SetCardData(newCardData.GetComponent<CardData>());
            playerHandUI1.GetComponent<BasicCard>().SetupUI();
            playerHandUI1.SetActive(true);
            if (playerDeck.Count() > 0)
            {
                GameObject newCardData2 = playerDeck.drawCard();
                playerHandUI2.GetComponent<BasicCard>().SetCardData(newCardData2.GetComponent<CardData>());
                playerHandUI2.GetComponent<BasicCard>().SetupUI();
                playerHandUI2.SetActive(true);
            }
            if (playerDeck.Count() > 0)
            {
                GameObject newCardData3 = playerDeck.drawCard();
                playerHandUI3.GetComponent<BasicCard>().SetCardData(newCardData3.GetComponent<CardData>());
                playerHandUI3.GetComponent<BasicCard>().SetupUI();
                playerHandUI3.SetActive(true);
            }
        }
        else
        {
            playerTurnButton.GetComponent<Button>().interactable = false;
        }

        playerTurnButton.GetComponent<Button>().interactable = false;
    }

    public void CardClick(string callerName, string callerTag)
    {
        if(callerName == "PlayerHandCard1")
        {
            playerCurrentCard = playerHandUI1.GetComponent<BasicCard>().cardData.gameObject;
            playerDeck.addCard(playerHandUI2.GetComponent<BasicCard>().cardData.gameObject);
            playerDeck.addCard(playerHandUI3.GetComponent<BasicCard>().cardData.gameObject);

        }
        if (callerName == "PlayerHandCard2")
        {
            playerCurrentCard = playerHandUI2.GetComponent<BasicCard>().cardData.gameObject;
            playerDeck.addCard(playerHandUI1.GetComponent<BasicCard>().cardData.gameObject);
            playerDeck.addCard(playerHandUI3.GetComponent<BasicCard>().cardData.gameObject);

        }
        if (callerName == "PlayerHandCard3")
        {
            playerCurrentCard = playerHandUI3.GetComponent<BasicCard>().cardData.gameObject;
            playerDeck.addCard(playerHandUI2.GetComponent<BasicCard>().cardData.gameObject);
            playerDeck.addCard(playerHandUI1.GetComponent<BasicCard>().cardData.gameObject);

        }
        playerHandUI1.SetActive(false);
        playerHandUI2.SetActive(false);
        playerHandUI3.SetActive(false);
        playerCurrentCardUI.GetComponent<BasicCard>().SetCardData(playerCurrentCard.GetComponent<CardData>());
        playerCurrentCardUI.GetComponent<BasicCard>().SetupUI();
        playerCurrentCardUI.SetActive(true);

        waitingForNextTurn = true;
        currentTurnEndWait = MAX_TURN_END_WAIT;
    }

    public void StartCombat()
    {
        Debug.Log("enemy turn");
        startButton.GetComponent<Button>().interactable = false;
        playerTurnButton.GetComponent<Button>().interactable = true;

        EnemyDraw();
    }

    public void combatResolution()
    {
        Debug.Log("combat resolution");


        playerAnimator.SetTrigger("attack");//Move to combat actuation
        enemyAnimator.SetTrigger("attack");
        while (currentTurnEndWait > 0)
        {

        }
        int pAttack = playerCurrentCard.GetComponent<CardData>().attackPower;
        int pDefencebonus = playerCurrentCard.GetComponent<CardData>().defenceBonus;
        int eAttack = enemyCurrentCard.GetComponent<CardData>().attackPower;
        int eDefenceBonus = enemyCurrentCard.GetComponent<CardData>().defenceBonus;

        //adds defence bonuses before combat from cards
        if (eDefenceBonus != 0) {
            enemy.defenceBonus += eDefenceBonus;
        }
        if (pDefencebonus != 0 ) {
            player.defenceBonus += pDefencebonus;
        }
        //speed check
        /*
        if (player.speed >= enemy.speed) {
            enemy.health = enemy.health - Mathf.Max((pAttack - (enemy.defence+enemy.defenceBonus)), 0);
            // place health check here end combat if health is at or below 0
            if (enemy.health <= 0) {
                return;
            }
            player.health = player.health - Mathf.Max((eAttack - (player.defence + player.defenceBonus)), 0);
            if (player.health <= 0) {
                return;
            }
        }
        else {
            player.health = player.health - Mathf.Max((eAttack - (player.defence + player.defenceBonus)), 0);

            // place health check here end combat if health is at or below 0
            if (player.health <= 0) {
                return;
            }

            enemy.health = enemy.health - Mathf.Max((pAttack - (enemy.defence + enemy.defenceBonus)), 0);
            if (enemy.health <= 0) {
                return;
            }
        }*/


        //reset defence bonuses at end of combat
        enemy.defenceBonus = 0;
        player.defenceBonus = 0;
        if (enemyCurrentCard != null) {
            //enemyCurrentCard.transform.localPosition = enemyDiscardPos;
            Debug.Log(enemyCurrentCard.GetComponent<CardData>().cardTitle);
            enemyDiscard.Add(enemyCurrentCard);
            enemyDiscardUI.GetComponent<BasicCard>().SetCardData(enemyCurrentCard.GetComponent<CardData>());
            enemyDiscardUI.GetComponent<BasicCard>().SetupUI();
            enemyDiscardUI.SetActive(true);
            enemyCurrentCardUI.SetActive(false);
        }
        if (playerCurrentCard != null) {
            //playerCurrentCard.transform.localPosition = playerDiscardPos;
            playerDiscard.Add(playerCurrentCard);
            playerDiscardUI.GetComponent<BasicCard>().SetCardData(playerCurrentCard.GetComponent<CardData>());
            playerDiscardUI.GetComponent<BasicCard>().SetupUI();
            playerDiscardUI.SetActive(true);
            playerCurrentCardUI.SetActive(false);
        }


        EnemyDraw();
    }

    public void EnemyDraw()
    {
        Debug.Log("enemy draw");
        enemyCurrentCard = enemyDeck.drawCard();
        enemyCurrentCardUI.GetComponent<BasicCard>().SetCardData(enemyCurrentCard.GetComponent<CardData>());
        enemyCurrentCardUI.GetComponent<BasicCard>().SetupUI();
        enemyCurrentCardUI.SetActive(true);


        playerTurnButton.GetComponent<Button>().interactable = true;
    }
}
