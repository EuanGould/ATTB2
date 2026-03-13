using UnityEngine;

public class RuinousIdeals : CardBehaviour
{
    public override void Play()
    {
        player_stats.MultiplyAttackMult(3);
        FinishPlaying();
    }
}
