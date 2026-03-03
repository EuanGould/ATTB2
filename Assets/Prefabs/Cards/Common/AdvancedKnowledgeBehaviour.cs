using UnityEngine;

public class AdvancedKnowledgeBehaviour : CardBehaviour
{
    public override void Play()
    {
        DrawNewCard();
        DrawNewCard();
        DrawNewCard();
        Discard();
    }
}
