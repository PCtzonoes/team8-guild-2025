using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolumonBlind : BossBlind
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GetRandomPlayerCards(3);
        }
    }

    public void GetRandomPlayerCards(int thisMany)
    {
        // list of all used indexes
        List<int> usedInts = new List<int>();

        // iterate for as many cards as Dolumon want to desguise
        for(int i = 0; i < thisMany; i++)
        {
            // random card not already chosen
            int randomInt = Random.Range(0, _playerHand.cardsInHand.Count);
            while (usedInts.Contains(randomInt) == true)
            {
                randomInt = Random.Range(0, _playerHand.cardsInHand.Count);
            }
            usedInts.Add(randomInt);
            Card card = _playerHand.cardsInHand[randomInt];

            // desguise it
            DesguiseCard(card);
        }
    }

    private void DesguiseCard(Card card)
    {
        int initialRank = card.cardRank;
        string intialSuit = card.cardSuit;

        // select a random rank between 2 and 14
        card.cardRank = Random.Range(2, 15);

        // select a random suit
        int randomFour = Random.Range(1, 5);
        switch (randomFour)
        {
            case 1:
                card.cardSuit = "clubs";
                break;
            case 2:
                card.cardSuit = "diamonds";
                break;
            case 3:
                card.cardSuit = "spades";
                break;
            case 4:
                card.cardSuit = "hearts";
                break;
        }

        // render disguise
        card.RenderCard();

        // switch back to the old true values and play vfx
        card.cardSuit = intialSuit;
        card.cardRank = initialRank;
        card.status = CardStatus.Desguised;
    }
}
