using UnityEngine;
using UnityEngine.Rendering;

public class SecondAttemptBehaviour : CardBehaviour
{
    public override void Play()
    {
        int cards_to_draw = 0;

        foreach (CardBehaviour card in GameObject.FindGameObjectWithTag("PlayerHand").GetComponentsInChildren<CardBehaviour>())
        {
            if (card != this)
            {
                cards_to_draw++;
                card.Discard();
            }
        }

        for (int i = 0; i < cards_to_draw; i++)
        {
            DrawNewCard();
        }

        FinishPlaying();
    }
}
