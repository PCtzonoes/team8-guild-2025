using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Powers
{
    public class ImpPower : MonoBehaviour, IPower, ICardTransformer
    {
        public TrickManager TrickManager { get; set; }

        public string Name { get; } = "Damnation";

        public string Description  {get; } = "Turn 1 card in hand into an Imp";

        public void TransformCard(Card card)
        {
            card.cardRank = 0;
            card.cardSuit = "imp";
            card.RenderCard();
        }

        public void Use(TrickManager trickManager)
        {
            TrickManager = trickManager;
        }
    }
}

