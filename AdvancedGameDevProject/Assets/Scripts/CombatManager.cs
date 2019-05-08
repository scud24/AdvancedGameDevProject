using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    public List<Sprite> heartSprites;

    public GameObject winPanel;
    public GameObject prizeCardUI;
    public Text prizeGoldText;
    public GameObject losePanel;
    public PackTable pt;
    // Start is called before the first frame update
    void Start()
    {
        pgm = GameObject.Find("PersistentGameManager");
        //Debug.Log("Player set");
        player.SetupFromPlayerData(pgm.GetComponent<PersistentGameManager>().playerData);
        //Debug.Log("Player deck set");
        playerDeck.SetupFromList(player.currentDeck);
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isGrounded", true);

        instanceManager = GameObject.Find("InstanceManager");
        enemyManager = GameObject.Find("EnemyManager");
        int enemyIndex = instanceManager.GetComponent<InstanceManager>().enemyIndex;
        Debug.Log("enemy set - index:" + enemyIndex.ToString());
        enemy.SetupFromPlayerData(enemyManager.GetComponent<EnemyManager>().enemyList[enemyIndex]);
        GameObject enemyModel = enemyManager.GetComponent<EnemyManager>().GetEnemybyIndex(enemyIndex);
        enemyAnimator = enemyModel.GetComponent<Animator>();
        enemyModel.SetActive(true);


        Debug.Log("enemy deck set");
        enemyDeck.SetupFromList(enemy.currentDeck);
        Debug.Log("Player deck count: " + playerDeck.Count() + " Enemy deck count: " + enemyDeck.Count());

        UpdateHeartUI();

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

    public void LosePanelContinue()
    {

        pgm = GameObject.Find("PersistentGameManager");
        pgm.GetComponent<PersistentGameManager>().SetInProgress(false);
        Debug.Log("Exiting by lose");
        instanceManager = GameObject.Find("InstanceManager");
        Destroy(instanceManager.gameObject);

        FindObjectOfType<AudioManager>().PlayMusic(0);
        SceneManager.LoadScene("Map Menu");
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
        //FindObjectOfType<AudioManager>().StopSounds();
        FindObjectOfType<AudioManager>().PlayMusic(1);
        Debug.Log("enemy turn");
        startButton.GetComponent<Button>().interactable = false;
        playerTurnButton.GetComponent<Button>().interactable = true;

        EnemyDraw();
    }

    public void PlayAttackAnimations()
    {
        playerAnimator.SetTrigger(playerCurrentCard.GetComponent<CardData>().attackAnimation);
        enemyAnimator.SetTrigger(enemyCurrentCard.GetComponent<CardData>().attackAnimation);
        //enemyAnimator.SetTrigger("basic_attack");
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
        Debug.Log("Player: " + player.currentHealth  + " Enemy: " + enemy.currentHealth );
        UpdateHeartUI();


        CheckWinner();
        if (enemyCurrentCard != null) {
            //enemyCurrentCard.transform.localPosition = enemyDiscardPos;
            //Debug.Log(enemyCurrentCard.GetComponent<CardData>().cardTitle);
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
        if(playerDeck.Count() <= 0)
        {
            while(playerDiscard.Count > 0)
            {
                playerDeck.addCard(playerDiscard[0]);
                playerDiscard.RemoveAt(0);
            }
            playerDeck.shuffle();
        }
        if (enemyDeck.Count() <= 0)
        {
            while (enemyDiscard.Count > 0)
            {
                enemyDeck.addCard(enemyDiscard[0]);
                enemyDiscard.RemoveAt(0);
            }
            enemyDeck.shuffle();
        }
        EnemyDraw();
    }

    public void EnemyDraw()
    {
        //Debug.Log("enemy draw");
        enemyCurrentCard = enemyDeck.drawCard();
        enemyCurrentCardUI.GetComponent<BasicCard>().SetCardData(enemyCurrentCard.GetComponent<CardData>());
        enemyCurrentCardUI.GetComponent<BasicCard>().SetupUI();
        enemyCurrentCardUI.SetActive(true);


        playerTurnButton.GetComponent<Button>().interactable = true;
        PlayerTurn();
    }
    public void UpdateHeartUI()
    {
        int playerHeartShards = player.currentHealth % 4;
        int playerCurrentFullHearts = ((player.currentHealth - playerHeartShards) / 4);
        Debug.Log("Player hearts: " + playerCurrentFullHearts + " Player Shards: " + playerHeartShards);
        if(playerHeartShards < 0)
        {
            playerHeartShards = 0;
        }
        for (int i = 0; i <= 4; i++)
        {
            if(i > playerCurrentFullHearts)
            {
                //Debug.Log("Player Heart at index " + i + " set to " + 0);
                playerHearts[i].GetComponent<Image>().sprite = heartSprites[0];
            }            
            else if(i < playerCurrentFullHearts)
            {
                //Debug.Log("Player Heart at index " + i + " set to " + 4);
                playerHearts[i].GetComponent<Image>().sprite = heartSprites[4];
            }
            else
            {
                //Debug.Log("Player Heart at index " + i + " set to " + playerHeartShards);
                playerHearts[i].GetComponent<Image>().sprite = heartSprites[playerHeartShards];
            }
        }

        int enemyHeartShards = enemy.currentHealth % 4;
        int enemyCurrentFullHearts = ((enemy.currentHealth - enemyHeartShards) / 4);
        Debug.Log("Enemy hearts: " + enemyCurrentFullHearts + " Enemy Shards: " + enemyHeartShards);
        if (enemyHeartShards < 0)
        {
            enemyHeartShards = 0;
        }
        for (int i = 0; i <= 4; i++)
        {
            if (i > enemyCurrentFullHearts)
            {
                //Debug.Log("Enemy Heart at index " + i + " set to " + 0);
                enemyHearts[i].GetComponent<Image>().sprite = heartSprites[0];
            }
            else if (i < enemyCurrentFullHearts)
            {
                //Debug.Log("Enemy Heart at index " + i + " set to " + 4);
                enemyHearts[i].GetComponent<Image>().sprite = heartSprites[4];
            }
            else
            {
                //Debug.Log("Enemy Heart at index " + i + " set to " + enemyHeartShards);
                enemyHearts[i].GetComponent<Image>().sprite = heartSprites[enemyHeartShards];
            }
        }

    }

    public void CheckWinner()
    {
        if (enemy.currentHealth <= 0)
        {
            CardData prizeCard = pt.rollSpecialCard();
            prizeCardUI.GetComponent<BasicCard>().SetCardData(prizeCard);
            prizeCardUI.GetComponent<BasicCard>().SetupUI();
            pgm.GetComponent<PersistentGameManager>().playerData.cardInventory.Add(prizeCard);
            prizeCardUI.SetActive(true);
            int prizeGold = 10 + Random.Range(0, 30);
            prizeGoldText.text = "Gold Found: " + prizeGold.ToString();
            pgm.GetComponent<PersistentGameManager>().playerData.gold += prizeGold;
            winPanel.SetActive(true);
        }
        else if(player.currentHealth <=0)
        {
            losePanel.SetActive(true);
        }
    }
    public void WinPanelContinue()
    {
        SceneManager.LoadScene("environment");
    }
}
