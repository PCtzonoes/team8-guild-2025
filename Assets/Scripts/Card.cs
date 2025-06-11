using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public int cardRank;
    public string cardSuit;
    public bool isAvailable;
    public bool isSelected;
    public bool isInHand = false;

    // hovering over the card when it's in hand.
    private void OnMouseEnter()
    {
        if (isInHand)
        {

        }
    }

    private void OnMouseExit()
    {
        if (isInHand)
        {
            if(!isSelected)
            {

            }
        }
    }

}
