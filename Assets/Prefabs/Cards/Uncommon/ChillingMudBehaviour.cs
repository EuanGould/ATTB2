using UnityEngine;

public class ChillingMudBehaviour : CardBehaviour
{
    public override void Play()
    {
        Target();
    }

    public override void targetPayoff(EnemyBehaviour enemy)
    {
        enemy.DelayAttack(4);
        enemy.damage(5);
        player_stats.ExpendAttackMult();
        FinishPlaying();
    }
}
