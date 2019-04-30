using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackTable : MonoBehaviour
{
    [SerializeField]
    private CardsForPacks[] cardPool;
    [SerializeField]
    private CardData[] cardPack = new CardData[5];



    private void rollCards() {
        for (int i = 0; i < 5; i++) {
            int index = Random.Range(8, 23);
            cardPack[i] = (cardPool[index].Card);

        }
    }

    private void rollSpecialCards() {
        int index = Random.Range(0, 8);
        cardPack[0] = (cardPool[index].Card);
        for (int i = 1; i < 5; i++) {
            index = Random.Range(8, 23);
            cardPack[i] = (cardPool[index].Card);

        }

    }


}
