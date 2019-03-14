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
            GameObject newCard2 = Instantiate(playerDeck.drawCard());
            playerHand.addCard(newCard2);
            GameObject newCard3 = Instantiate(playerDeck.drawCard());
            playerHand.addCard(newCard3);
            newCard.transform.SetParent(playArea.transform);
            newCard2.transform.SetParent(playArea.transform);
            newCard3.transform.SetParent(playArea.transform);
            newCard.tag = "Player";
            newCard2.tag = "Player";
            newCard3.tag = "Player";
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
                if(playerCurrentCard != null)
                {
                    playerCurrentCard.transform.localPosition = playerDiscardPos;
                    playerDiscard.Add(playerCurrentCard);
                }
                playerCurrentCard = playerHand.playCard(callbackIndex);
                playerCurrentCard.transform.localPosition = playerSelectedCardPos;
                playerCurrentCard.name = "PlayerCurrentCard";
                for (int i = 0; i < playerHand.Count(); i++)
                {
                    Vector3 tempPos = new Vector3(playerHandPos.x, playerHandPos.y, playerHandPos.z);
                    tempPos.x = tempPos.x + (playerHandOffset * i);
                    playerHand.getCardAtIndex(i).transform.localPosition = tempPos;
                    playerHand.getCardAtIndex(i).name = i.ToString();
                }
            }
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
        }
    }

    public void EnemyTurn()
    {
        Debug.Log(enemyDeck.Count());
        if (enemyDeck.Count() > 0)
        {
            GameObject newCard = Instantiate(enemyDeck.drawCard());
            enemyHand.addCard(newCard);
            GameObject newCard2 = Instantiate(enemyDeck.drawCard());
            enemyHand.addCard(newCard2);
            GameObject newCard3 = Instantiate(enemyDeck.drawCard());
            enemyHand.addCard(newCard3);
            newCard.transform.SetParent(playArea.transform);
            newCard2.transform.SetParent(playArea.transform);
            newCard3.transform.SetParent(playArea.transform);
            newCard.tag = "Enemy";
            newCard2.tag = "Enemy";
            newCard3.tag = "Enemy";
        }
        enemyHandOffset = enemyHandWidth / enemyHand.Count();
        for (int i = 0; i < enemyHand.Count(); i++)
        {
            Vector3 tempPos = new Vector3(enemyHandPos.x, enemyHandPos.y, enemyHandPos.z);
            tempPos.x = tempPos.x + (enemyHandOffset * i);
            enemyHand.getCardAtIndex(i).transform.localPosition = tempPos;
            enemyHand.getCardAtIndex(i).name =  i.ToString();
        }
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

    public void EnemyAction()
    {
        int callbackIndex;
        if (int.TryParse(EventSystem.current.currentSelectedGameObject.name, out callbackIndex))
        {
            enemyCurrentCard = enemyHand.playCard(callbackIndex);
            enemyCurrentCard.transform.position = enemySelectedCardPos;
        }
    }
}
