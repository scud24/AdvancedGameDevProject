using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public PlayerManagerScript player;
    public DeckManagerScript playerDeck;
    public HandManagerScript playerHand;
    public Vector3 playerHandPos;
    public float playerHandOffset;
    public float playerHandWidth;

    public PlayerManagerScript enemy;
    public DeckManagerScript enemyDeck;
    public HandManagerScript enemyHand;
    public Vector3 enemyHandPos;
    public float enemyHandOffset;
    public float enemyHandWidth;

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
            newCard.transform.SetParent(playArea.transform);
        }
        playerHandOffset = playerHandWidth / playerHand.Count();
        for(int i = 0; i < playerHand.Count(); i++)
        {
            Vector3 tempPos = new Vector3(playerHandPos.x, playerHandPos.y, playerHandPos.z);
            tempPos.x = tempPos.x + (playerHandOffset * i);
            playerHand.getCardAtIndex(i).transform.localPosition = tempPos;
        }
    }

    public void EnemyTurn()
    {
        Debug.Log(enemyDeck.Count());
        if (enemyDeck.Count() > 0)
        {
            GameObject newCard = Instantiate(enemyDeck.drawCard());
            enemyHand.addCard(newCard);
            newCard.transform.SetParent(playArea.transform);
        }
        enemyHandOffset = enemyHandWidth / enemyHand.Count();
        for (int i = 0; i < enemyHand.Count(); i++)
        {
            Vector3 tempPos = new Vector3(enemyHandPos.x, enemyHandPos.y, enemyHandPos.z);
            tempPos.x = tempPos.x + (enemyHandOffset * i);
            enemyHand.getCardAtIndex(i).transform.localPosition = tempPos;
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
}
