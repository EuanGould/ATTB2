using UnityEngine;

public class ObstinateGoldenManBehaviour : CardBehaviour
{
    public override void Play()
    {
        if (card_selection_manager.gameObject.GetComponentsInChildren<CardBehaviour>().Length == 1)
        {
            foreach (EnemyBehaviour enemy in GetEnemies())
            {
                enemy.damage(999999999);
            }

            player_stats.ExpendAttackMult();
        }
        FinishPlaying();
    }
}
