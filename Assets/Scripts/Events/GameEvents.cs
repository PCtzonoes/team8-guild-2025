using System;

namespace DefaultNamespace.Events
{
    public static class GameEvents
    {
        public static event Action<string> OnWildCardSetSuit;

        public static event Action<int> OnBetMade;

        public static event Action<Card> OnPlayedCard;

        public static event Action<bool> OnTrickEnd;

        public static event Action OnStartBetting;

        public static event Action<int> OnTrickWon;

        public static event Action<string> OnGameLost;

        public static event Action<string> OnGameWon;

        public static void SetWildCardSuit(string wildCardSuit) => OnWildCardSetSuit?.Invoke(wildCardSuit);
        
        public static void MadeBet(int targetTricksWins) => OnBetMade?.Invoke(targetTricksWins);
        
        public static void PlayedCard(Card card) => OnPlayedCard?.Invoke(card);
        
        public static void EndTrick(bool isPlayerWinner) => OnTrickEnd?.Invoke(isPlayerWinner);

        public static void StartBetting() => OnStartBetting?.Invoke();

        public static void TrickWon(int tricksWon) => OnTrickWon?.Invoke(tricksWon);

        public static void GameLost(string msg) => OnGameLost?.Invoke(msg);
        public static void GameWon(string msg) => OnGameWon?.Invoke(msg);
    }
}