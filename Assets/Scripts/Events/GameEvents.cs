using System;

namespace DefaultNamespace.Events
{
    public static class GameEvents
    {
        public static event Action<string> OnWildCardSetSuit;

        public static event Action<int> OnBetMade;

        public static event Action<Card> OnPlayedCard;

        public static event Action<bool> OnTrickEnd;

        public static void SetWildCardSuit(string wildCardSuit) => OnWildCardSetSuit?.Invoke(wildCardSuit);
        
        public static void MadeBet(int targetTricksWins) => OnBetMade?.Invoke(targetTricksWins);
        
        public static void PlayedCard(Card card) => OnPlayedCard?.Invoke(card);
        
        public static void EndTrick(bool isPlayerWinner) => OnTrickEnd?.Invoke(isPlayerWinner);
    }
}