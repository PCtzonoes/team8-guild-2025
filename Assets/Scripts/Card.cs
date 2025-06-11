using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public int cardRank;
    public string cardSuit;
    public bool isAvailable;
    public bool isInHand;

    // hovering over the card when it's in hand.
    private void OnMouseOver()
    {
        if (isInHand)
        {

        }
    }

}
