using UnityEngine;

public class DiabolicalPreparationsBehaviour : CardBehaviour
{
    public override void Play()
    {
        player_stats.AddAttackAdd(1);
        player_stats.MultiplyAttackMult(2);
        FinishPlaying();
    }
}
