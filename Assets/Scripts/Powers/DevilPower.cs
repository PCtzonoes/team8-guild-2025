using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Powers
{
    public class DevilPower : MonoBehaviour, IPower, ICardTransformer
    {
        public TrickManager TrickManager { get; set; }

        public string Name { get; } = "Ascension";

        public string Description { get; } = "Turn 1 card in hand into a Devil";

        public void TransformCard(Card card)
        {
            card.cardRank = 99;
            card.cardSuit = "devil";
            card.RenderCard();
        }

        public void Use(TrickManager trickManager)
        {
            TrickManager = trickManager;
        }
    }
}


