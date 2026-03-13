using UnityEngine;

public class PlannedDuoDialBehaviour : CardBehaviour
{
    public override void Play()
    {
        player_stats.AddAttackAdd(2);
        DrawNewCard();
        DrawNewCard();

        FinishPlaying();
    }
}
