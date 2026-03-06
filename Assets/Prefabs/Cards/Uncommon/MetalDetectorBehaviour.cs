using UnityEngine;

public class MetalDetectorBehaviour : CardBehaviour
{
    public override void Play()
    {
        CardBehaviour[] grave_cards = GameObject.FindGameObjectWithTag("DiscardPile").GetComponentsInChildren<CardBehaviour>();
        if (grave_cards.Length > 0)
        {
            grave_cards[Random.Range(0, grave_cards.Length)].transform.SetParent(GameObject.FindGameObjectWithTag("PlayerHand").transform);
            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetHandSelection();
        }

        grave_cards = GameObject.FindGameObjectWithTag("DiscardPile").GetComponentsInChildren<CardBehaviour>();
        if (grave_cards.Length > 0)
        {
            grave_cards[Random.Range(0, grave_cards.Length)].transform.SetParent(GameObject.FindGameObjectWithTag("PlayerHand").transform);
            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetHandSelection();
        }

        grave_cards = GameObject.FindGameObjectWithTag("DiscardPile").GetComponentsInChildren<CardBehaviour>();
        if (grave_cards.Length > 0)
        {
            grave_cards[Random.Range(0, grave_cards.Length)].transform.SetParent(GameObject.FindGameObjectWithTag("PlayerHand").transform);
            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetHandSelection();
        }

        FinishPlaying();
    }
}
