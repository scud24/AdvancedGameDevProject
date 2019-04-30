using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatManager : MonoBehaviour
{
    public PlayerManagerScript player;
    public DeckManagerScript playerDeck;
    public CardData playerCurrentCard;
    public GameObject playerCurrentCardUI;
    public List<CardData> playerDiscard;
    public GameObject playerDiscardUI;
    public GameObject playerHandUI1;
    public GameObject playerHandUI2;
    public GameObject playerHandUI3;
    public List<GameObject> playerHearts;

    public PlayerManagerScript enemy;
    public DeckManagerScript enemyDeck;
    public HandManagerScript enemyHand;
    public CardData enemyCurrentCard;
    public GameObject enemyCurrentCardUI;
    public List<CardData> enemyDiscard;
    public GameObject enemyDiscardUI;
    public List<GameObject> enemyHearts;

    public List<GameObject> dummyCards;
    public float MAX_TURN_END_WAIT;
    public float currentTurnEndWait;
    public bool waitingForNextTurn;
    public GameObject playArea;
    public GameObject endPanel;
    public GameObject endPanelText;
    public GameObject startButton;
    public GameObject playerTurnButton;


    public List<GameObject> enemies;
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public GameObject pgm;
    public GameObject instanceManager;
    public GameObject enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        pgm = GameObject.Find("PersistantGameManager");
        Debug.Log("Player set");
        player.SetupFromPlayerData(pgm.GetComponent<PersistentGameManager>().playerData);
        Debug.Log("Player deck set");
        playerDeck.SetupFromList(player.currentDeck);


        instanceManager = GameObject.Find("InstanceManager");
        enemyManager = GameObject.Find("EnemyManager");
        Debug.Log("enemy set - index:" + instanceManager.GetComponent<InstanceManager>().enemyIndex.ToString());
        enemy.SetupFromPlayerData(enemyManager.GetComponent<EnemyManager>().enemyList[instanceManager.GetComponent<InstanceManager>().enemyIndex]);//TODO ENEMY MANAGER

        Debug.Log("enemy deck set");
        enemyDeck.SetupFromList(player.currentDeck);
        Debug.Log(playerDeck.Count());
        Debug.Log(enemyDeck.Count());

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
            CardData newCardData = playerDeck.drawCard();
            playerHandUI1.GetComponent<BasicCard>().SetCardData(newCardData);
            playerHandUI1.GetComponent<BasicCard>().SetupUI();
            playerHandUI1.SetActive(true);
            if (playerDeck.Count() > 0)
            {
                CardData newCardData2 = playerDeck.drawCard();
                playerHandUI2.GetComponent<BasicCard>().SetCardData(newCardData2);
                playerHandUI2.GetComponent<BasicCard>().SetupUI();
                playerHandUI2.SetActive(true);
            }
            if (playerDeck.Count() > 0)
            {
                CardData newCardData3 = playerDeck.drawCard();
                playerHandUI3.GetComponent<BasicCard>().SetCardData(newCardData3);
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
            playerCurrentCard = playerHandUI1.GetComponent<BasicCard>().cardData;
            playerDeck.addCard(playerHandUI2.GetComponent<BasicCard>().cardData);
            playerDeck.addCard(playerHandUI3.GetComponent<BasicCard>().cardData);

        }
        if (callerName == "PlayerHandCard2")
        {
            playerCurrentCard = playerHandUI2.GetComponent<BasicCard>().cardData;
            playerDeck.addCard(playerHandUI1.GetComponent<BasicCard>().cardData);
            playerDeck.addCard(playerHandUI3.GetComponent<BasicCard>().cardData);

        }
        if (callerName == "PlayerHandCard3")
        {
            playerCurrentCard = playerHandUI3.GetComponent<BasicCard>().cardData;
            playerDeck.addCard(playerHandUI2.GetComponent<BasicCard>().cardData);
            playerDeck.addCard(playerHandUI1.GetComponent<BasicCard>().cardData);

        }
        playerHandUI1.SetActive(false);
        playerHandUI2.SetActive(false);
        playerHandUI3.SetActive(false);
        playerCurrentCardUI.GetComponent<BasicCard>().SetCardData(playerCurrentCard.GetComponent<CardData>());
        playerCurrentCardUI.GetComponent<BasicCard>().SetupUI();
        playerCurrentCardUI.SetActive(true);

        waitingForNextTurn = true;
        currentTurnEndWait = MAX_TURN_END_WAIT;
        PlayAttackAnimations();
    }

    public void StartCombat()
    {
        Debug.Log("enemy turn");
        startButton.GetComponent<Button>().interactable = false;
        playerTurnButton.GetComponent<Button>().interactable = true;

        EnemyDraw();
    }

    public void PlayAttackAnimations()
    {
        playerAnimator.SetTrigger(playerCurrentCard.GetComponent<CardData>().attackAnimation);
        //enemyAnimator.SetTrigger(enemyCurrentCard.GetComponent<CardData>().attackAnimation);
        enemyAnimator.SetTrigger("basic_attack");
    }

    public void combatResolution()
    {
        Debug.Log("combat resolution");


       

        int pAttack = playerCurrentCard.attackPower;
        int pDefenceBonus = playerCurrentCard.defenceBonus;
        int eAttack = enemyCurrentCard.attackPower;
        int eDefenceBonus = enemyCurrentCard.defenceBonus;


        enemy.DamagePlayer(pAttack - eDefenceBonus);
        player.DamagePlayer(eAttack - pDefenceBonus);


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
