using UnityEngine;

public class PerformanceCastingBehaviour : CardBehaviour
{
    public override void Play()
    {
        DrawNewCard();
        DrawNewCard();
        Discard();
    }
}
