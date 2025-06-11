using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<Card> cardsInHand = new List<Card>();

    [SerializeField] private float cardSpacingX = 1f;
    [SerializeField] private float cardSpacingZ = .01f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ArrangeHand();
        }
    }

    public void ArrangeHand()
    {
        int count = cardsInHand.Count;
        if (count == 0) return;


        float spacingX = cardSpacingX/count;

        float totalWidth = (count - 1) * spacingX;
        float startX = -totalWidth / 2f; // Center the hand

        float totalDepth = (count - 1) * cardSpacingZ;
        float startZ = totalDepth / 2f; // Centre of hand

        for (int i = 0; i < count; i++)
        {
            Card thisCard = cardsInHand[i];
            thisCard.transform.SetParent(transform);

            // change position based on order
            Vector3 newPos = new Vector3(startX + i * spacingX, 0f, startZ + i * - cardSpacingZ);
            thisCard.transform.localPosition = newPos;

            // rotate based on position
            thisCard.transform.rotation = transform.rotation;
        }
    }
}
