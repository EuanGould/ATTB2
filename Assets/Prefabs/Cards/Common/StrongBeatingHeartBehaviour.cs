using UnityEngine;

public class StrongBeatingHeartBehaviour : CardBehaviour
{
    public override void Play()
    {
        player_stats.AddAttackAdd(1);
        DrawNewCard();
        FinishPlaying();
    }
}
