using DefaultNamespace.Events;
using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private GameManager _gameManager;
    private DeckManager _deckManager;

    private bool _playerContinued = false;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _deckManager = FindObjectOfType<DeckManager>();
    }

    public void StartTutorial()
    {
        StartCoroutine(StartTutorialRoutine());
    }

    private IEnumerator StartTutorialRoutine()
    {
        _deckManager.ShuffleCards();
        yield return WaitForPlayerContinue();

        _gameManager.SetTrumpCard();
        while (_gameManager.wildCardSuit != "hearts" && _gameManager.wildCardSuit != "clubs" && _gameManager.wildCardSuit != "diamonds" && _gameManager.wildCardSuit != "spades")
        {
            yield return new WaitForSeconds(1.0f);
            _gameManager.SetTrumpCard();
        }
        yield return WaitForPlayerContinue();

        _gameManager.DrawPlayerHand();

        yield return WaitForPlayerContinue();

        GameEvents.StartBetting();
    }

    private IEnumerator WaitForPlayerContinue()
    {
        _playerContinued = false;
        yield return new WaitUntil(() => _playerContinued);
    }

    public void OnPlayerPressedContinue()
    {
        _playerContinued = true;
    }

    public void SetTrumpCard()
    {
        //TODO: Handler redraw if is Wizard or Jester
        Card wildCard = _deckManager.DrawCardsFromDeck(1)[0];

        if (wildCard.cardSuit != "hearts" && wildCard.cardSuit != "clubs" && wildCard.cardSuit != "diamonds" && wildCard.cardSuit != "spades")
        {
            wildCard.cardSuit = "spades";
            wildCard.cardRank = 14;
            wildCard.RenderCard();
        }

        _gameManager.wildCardSuit = wildCard.cardSuit;

        wildCard.transform.SetParent(transform);
        wildCard.AnimOnMoveAndRotate(_gameManager.WildCardPosition, _gameManager.WildCardRotation, 0.1f);

        GameEvents.SetWildCardSuit(_gameManager.wildCardSuit);

        StartCoroutine(DelayDiscard(wildCard));
    }

    public void DrawImp()
    {
        Card imp = _deckManager.DrawCardsFromDeck(1)[0];

        imp.cardRank = 0;
        imp.cardSuit = "imp";
        imp.RenderCard();
        imp.transform.SetParent(transform);
        imp.AnimOnMoveAndRotate(_gameManager.WildCardPosition, _gameManager.WildCardRotation, 0.1f);
        StartCoroutine(DelayDiscard(imp));
    }

    public void DrawDevil()
    {
        Card devil = _deckManager.DrawCardsFromDeck(1)[0];

        devil.cardRank = 99;
        devil.cardSuit = "devil";
        devil.RenderCard();
        devil.transform.SetParent(transform);
        devil.AnimOnMoveAndRotate(_gameManager.WildCardPosition, _gameManager.WildCardRotation, 0.1f);
        StartCoroutine(DelayDiscard(devil));
    }

    private IEnumerator DelayDiscard(Card card)
    {
        yield return WaitForPlayerContinue();
        card.AnimOnMoveAndRotate(_gameManager.DiscardPosition, Quaternion.identity, 0f);
    }


}
