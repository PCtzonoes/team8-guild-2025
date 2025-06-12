using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private PlayerHand playerHand;

    private string _wildCardSuit;
    
    private void Start()
    {
        
    }

    private IEnumerable PreBetRoutine()
    {
        this.SetWildCard();
    }

    private IEnumerable SetWildCard()
    {
        //TODO: Handler redraw if is Wizard or Jester
         Card wildCard = deckManager.DrawCards(1)[0];
         _wildCardSuit = wildCard.cardSuit;
         
         wildCard.
         
    }
}
