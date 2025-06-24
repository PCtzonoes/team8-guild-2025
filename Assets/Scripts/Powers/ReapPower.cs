using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Powers
{
    public class ReapPower : MonoBehaviour, IPower, ICardTransformer
    {
        public string Name { get; } = "Reap";
        
        public string Description { get; } = "Destroy a target card.";

        public TrickManager TrickManager { get; set; }
        
        public void Use(TrickManager trickManager)
        {
            TrickManager = trickManager;
        }
        
        public void TransformCard(Card card)
        {
            TrickManager.DiscardCards(new List<Card>() {card});
            card.transform.localPosition = TrickManager._graveyard;
        }
    }
}