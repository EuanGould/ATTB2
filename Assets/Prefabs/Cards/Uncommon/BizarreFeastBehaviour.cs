using UnityEngine;

public class BizarreFeastBehaviour : CardBehaviour
{
    public override void Play()
    {
        foreach (EnemyBehaviour enemy in GetEnemies())
        {
            enemy.damage(2);
            player_stats.Heal(2);
        }

        player_stats.ExpendAttackMult();
        FinishPlaying();
    }
}
