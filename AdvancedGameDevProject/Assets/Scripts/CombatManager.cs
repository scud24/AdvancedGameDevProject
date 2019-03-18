using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatManager : MonoBehaviour
{
    public PlayerManagerScript player;
    public DeckManagerScript playerDeck;
    public HandManagerScript playerHand;
    public Vector3 playerHandPos;
    public Vector3 playerSelectedCardPos;
    public Vector3 playerDiscardPos;
    public float playerHandOffset;
    public float playerHandWidth;
    public GameObject playerCurrentCard;
    public List<GameObject> playerDiscard;

    public PlayerManagerScript enemy;
    public DeckManagerScript enemyDeck;
    public HandManagerScript enemyHand;
    public Vector3 enemyHandPos;
    public Vector3 enemySelectedCardPos;
    public Vector3 enemyDiscardPos;
    public float enemyHandOffset;
    public float enemyHandWidth;
    public GameObject enemyCurrentCard;
    public List<GameObject> enemyDiscard;

    public List<GameObject> dummyCards;

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
        playerHandWidth = Mathf.Abs(playerHandPos.x * 2);
        enemyHandWidth = Mathf.Abs(enemyHandPos.x * 2);
        for(int i = 0; i < dummyCards.Count; i++)
        {
            playerDeck.addCard(dummyCards[i]);
            //Debug.Log(playerDeck.Count());
            enemyDeck.addCard(dummyCards[i]);
            //Debug.Log(enemyDeck.Count());
        }
        //playerDeck.shuffle();
        //enemyDeck.shuffle();
        Debug.Log(playerDeck.Count());
        Debug.Log(enemyDeck.Count());
        //EnemySetup();
        enemy.health = 10;
        player.health = 10;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void PlayerTurn()
    {
        if (playerDeck.Count() > 0)
        {
            GameObject newCard = Instantiate(playerDeck.drawCard());
            playerHand.addCard(newCard);
            newCard.tag = "Player";
            newCard.SetActive(true);
            newCard.transform.SetParent(playArea.transform);
            if (playerDeck.Count() > 0)
            {
                GameObject newCard2 = Instantiate(playerDeck.drawCard());
                playerHand.addCard(newCard2);
                newCard2.tag = "Player";
                newCard2.transform.SetParent(playArea.transform);
                newCard2.SetActive(true);
            }
            if (playerDeck.Count() > 0)
            {
                GameObject newCard3 = Instantiate(playerDeck.drawCard());
                playerHand.addCard(newCard3);
                newCard3.transform.SetParent(playArea.transform);
                newCard3.tag = "Player";
                newCard3.SetActive(true);
            }
        }
        else
        {
            playerTurnButton.GetComponent<Button>().interactable = false;
        }
        playerHandOffset = playerHandWidth / playerHand.Count();
        for(int i = 0; i < playerHand.Count(); i++)
        {
            Vector3 tempPos = new Vector3(playerHandPos.x, playerHandPos.y, playerHandPos.z);
            tempPos.x = tempPos.x + (playerHandOffset * i);
            playerHand.getCardAtIndex(i).transform.localPosition = tempPos;
            playerHand.getCardAtIndex(i).name =  i.ToString();
        }
    }

    public void CardClick(string callerName, string callerTag)
    {
        int callbackIndex;
        if( int.TryParse(callerName, out callbackIndex))
        {
            if(callerTag == "Player")
            {
                /*
                if(playerCurrentCard != null)
                {
                    playerCurrentCard.transform.localPosition = playerDiscardPos;
                    playerDiscard.Add(playerCurrentCard);
                }
                */
                playerAnimator.SetTrigger("attack");
                playerCurrentCard = playerHand.playCard(callbackIndex);
                playerCurrentCard.transform.localPosition = playerSelectedCardPos;
                playerCurrentCard.name = "PlayerCurrentCard";
                for (int i = playerHand.Count()-1; i >= 0; i--)
                {
                    Debug.Log("Storing card " + i);
                    /*Vector3 tempPos = new Vector3(playerHandPos.x, playerHandPos.y, playerHandPos.z);
                    tempPos.x = tempPos.x + (playerHandOffset * i);
                    playerHand.getCardAtIndex(i).transform.localPosition = tempPos;
                    playerHand.getCardAtIndex(i).name = i.ToString();*/
                    GameObject tempCard = playerHand.playCard(i);
                    playerDeck.addCard(tempCard);
                    tempCard.SetActive(false);
                    
                }
            }
            combatResolution();
            /*
            else if (callerTag == "Enemy")
            {
                if(enemyCurrentCard != null)
                {
                    enemyCurrentCard.transform.localPosition = enemyDiscardPos;
                    enemyDiscard.Add(enemyCurrentCard);
                }
                enemyCurrentCard = enemyHand.playCard(callbackIndex);
                enemyCurrentCard.transform.localPosition = enemySelectedCardPos;
                enemyCurrentCard.name = "EnemyCurrentCard";
                for (int i = 0; i < enemyHand.Count(); i++)
                {
                    Vector3 tempPos = new Vector3(enemyHandPos.x, enemyHandPos.y, enemyHandPos.z);
                    tempPos.x = tempPos.x + (enemyHandOffset * i);
                    enemyHand.getCardAtIndex(i).transform.localPosition = tempPos;
                    enemyHand.getCardAtIndex(i).name = i.ToString();
                }
            }
            */
        }
    }

    public void EnemyTurn()
    {
        startButton.GetComponent<Button>().interactable = false;
        playerTurnButton.GetComponent<Button>().interactable = true;
        Debug.Log(enemyDeck.Count());
        if (enemyDeck.Count() > 0)
        {
            GameObject newCard = Instantiate(enemyDeck.drawCard());
            enemyHand.addCard(newCard);
            /*
            GameObject newCard2 = Instantiate(enemyDeck.drawCard());
            enemyHand.addCard(newCard2);
            GameObject newCard3 = Instantiate(enemyDeck.drawCard());
            enemyHand.addCard(newCard3);
            */
            newCard.transform.SetParent(playArea.transform);
            //newCard2.transform.SetParent(playArea.transform);
            //newCard3.transform.SetParent(playArea.transform);
            newCard.tag = "Enemy";
            //newCard2.tag = "Enemy";
            //newCard3.tag = "Enemy";
        }
        enemyHandOffset = enemyHandWidth / enemyHand.Count();
        for (int i = 0; i < enemyHand.Count(); i++)
        {
            Vector3 tempPos = new Vector3(enemyHandPos.x, enemyHandPos.y, enemyHandPos.z);
            tempPos.x = tempPos.x + (enemyHandOffset * i);
            enemyHand.getCardAtIndex(i).transform.localPosition = tempPos;
            enemyHand.getCardAtIndex(i).name =  i.ToString();
        }
        EnemyAction();
    }
    public void EnemySetup()
    {
        for (int i = 0; i < dummyCards.Count; i++)
        {
            enemyDeck.addCard(dummyCards[i]);
            //Debug.Log(enemyDeck.Count());
        }
        //enemyDeck.shuffle();
        Debug.Log(playerDeck.Count());
        Debug.Log("enemy " + enemyDeck.Count());
    }

    public void combatResolution() {
        int pAttack = playerCurrentCard.GetComponent<BasicCard>().attackPower;
        int pDefencebonus = playerCurrentCard.GetComponent<BasicCard>().defenceBonus;
        int eAttack = enemyCurrentCard.GetComponent<BasicCard>().attackPower;
        int eDefenceBonus = enemyCurrentCard.GetComponent<BasicCard>().defenceBonus;

        //adds defence bonuses before combat from cards
        if (eDefenceBonus != 0) {
            enemy.defenceBonus += eDefenceBonus;
        }
        if (pDefencebonus != 0 ) {
            player.defenceBonus += pDefencebonus;
        }
        //speed check
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
        }


        //reset defence bonuses at end of combat
        enemy.defenceBonus = 0;
        player.defenceBonus = 0;
        if (enemyCurrentCard != null) {
            enemyCurrentCard.transform.localPosition = enemyDiscardPos;
            enemyDiscard.Add(enemyCurrentCard);
        }
        if (playerCurrentCard != null) {
            playerCurrentCard.transform.localPosition = playerDiscardPos;
            playerDiscard.Add(playerCurrentCard);
        }
        EnemyTurn();
    }

    public void EnemyAction()
    {
        if (enemyCurrentCard != null)
        {
            enemyCurrentCard.transform.localPosition = enemyDiscardPos;
            enemyDiscard.Add(enemyCurrentCard);
        }
        enemyAnimator.SetTrigger("attack");
        if (enemyHand.Count() > 0)
        {
            enemyCurrentCard = enemyHand.playCard(0);
            enemyCurrentCard.transform.localPosition = enemySelectedCardPos;
            enemyCurrentCard.name = "EnemyCurrentCard";
        }
        for (int i = 0; i < enemyHand.Count(); i++)
        {
            Vector3 tempPos = new Vector3(enemyHandPos.x, enemyHandPos.y, enemyHandPos.z);
            tempPos.x = tempPos.x + (enemyHandOffset * i);
            enemyHand.getCardAtIndex(i).transform.localPosition = tempPos;
            enemyHand.getCardAtIndex(i).name = i.ToString();
        }
        //int callbackIndex;
        //if (int.TryParse(EventSystem.current.currentSelectedGameObject.name, out callbackIndex))
        //{
        //enemyCurrentCard = enemyHand.playCard(0);
        //    enemyCurrentCard.transform.position = enemySelectedCardPos;
        //}
    }
}
