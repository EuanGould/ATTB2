using UnityEngine;

public class BigShot3StepGuideBehaviour : CardBehaviour
{
    public override void Play()
    {
        foreach (CardBehaviour card in GameObject.FindGameObjectWithTag("DeckPile").GetComponentsInChildren<CardBehaviour>())
        {
            card.AddCost(-2);
        }

        DrawNewCard();

        FinishPlaying();
    }
}
