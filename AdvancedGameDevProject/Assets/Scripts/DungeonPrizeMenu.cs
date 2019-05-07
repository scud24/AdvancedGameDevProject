using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonPrizeMenu : MonoBehaviour
{

    public GameObject dungeonClearedPanel;

    public GameObject prizeCardUI;
    public GameObject prizeCardUI2;
    public GameObject prizeCardUI3;
    public Text prizeGoldText;
    public GameObject pgm;
    public PackTable pt;

    private void Awake()
    {
        pgm = GameObject.Find("PersistentGameManager");
    }
    public void showPrizeMenu()
    {
        
            Debug.Log(prizeCardUI.GetComponent<BasicCard>());
            CardData prizeCard = pt.rollSpecialCard();
            prizeCardUI.GetComponent<BasicCard>().SetCardData(prizeCard);
            prizeCardUI.GetComponent<BasicCard>().SetupUI();
            pgm.GetComponent<PersistentGameManager>().playerData.cardInventory.Add(prizeCard);
            prizeCardUI.SetActive(true);

            CardData prizeCard2 = pt.rollSpecialCard();
            prizeCardUI2.GetComponent<BasicCard>().SetCardData(prizeCard2);
            prizeCardUI2.GetComponent<BasicCard>().SetupUI();
            pgm.GetComponent<PersistentGameManager>().playerData.cardInventory.Add(prizeCard2);
            prizeCardUI2.SetActive(true);

            CardData prizeCard3 = pt.rollSpecialCard();
            prizeCardUI3.GetComponent<BasicCard>().SetCardData(prizeCard3);
            prizeCardUI3.GetComponent<BasicCard>().SetupUI();
            pgm.GetComponent<PersistentGameManager>().playerData.cardInventory.Add(prizeCard3);
            prizeCardUI3.SetActive(true);



            int prizeGold = 10 + Random.Range(0, 30);
            prizeGoldText.text = "Gold Found: " + prizeGold.ToString();
            pgm.GetComponent<PersistentGameManager>().playerData.gold += prizeGold;
            dungeonClearedPanel.SetActive(true);
     }
    public void DungeonEndContinue()
    {
        Debug.Log("DungeonEndContinue");
        SceneManager.LoadScene("Map Menu");
        pgm.GetComponent<PersistentGameManager>().dungeonInProgress = false;
        Destroy(gameObject);
    }
}
