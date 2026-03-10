using UnityEngine;

public class TediousNovelBehaviour : CardBehaviour
{
    public override void Play()
    {
        DrawNewCard();
        DrawNewCard();
        AddCost(2);
        FinishPlaying();
    }
}
