using UnityEngine;

public class SturdyTroulBehaviour : CardBehaviour
{
    public override void Play()
    {
        CardBehaviour[] grave_cards = GameObject.FindGameObjectWithTag("DiscardPile").GetComponentsInChildren<CardBehaviour>();
        if (grave_cards.Length > 0)
        {
            grave_cards[grave_cards.Length - 1].transform.SetParent(GameObject.FindGameObjectWithTag("PlayerHand").transform);
            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetHandSelection();
        }

        FinishPlaying();
    }
}
