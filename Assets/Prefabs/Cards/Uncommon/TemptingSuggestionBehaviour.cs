using UnityEngine;

public class TemptingSuggestionBehaviour : CardBehaviour
{
    public override void Play()
    {
        CardBehaviour card = GameObject.FindGameObjectWithTag("DeckPile").GetComponent<DeckPile>().DrawCard();
        card.SetCost(0);
        FinishPlaying();
    }
}
