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
        StartCoroutine(StartTurotialRoutine());
    }

    private IEnumerator StartTurotialRoutine()
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
}
