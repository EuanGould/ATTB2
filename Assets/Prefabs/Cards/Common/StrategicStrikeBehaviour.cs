using UnityEngine;

public class StrategicStrikeBehaviour : CardBehaviour
{
    public override void Play()
    {
        Target();
    }

    public override void targetPayoff(EnemyBehaviour enemy)
    {
        enemy.damage(3);
        DrawNewCard();
        player_stats.ExpendAttackMult();
        FinishPlaying();
    }
}
