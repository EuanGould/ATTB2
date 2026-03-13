using UnityEngine;

public class ObstinateGoldenManBehaviour : CardBehaviour
{
    public override void Play()
    {
        if (card_selection_manager.gameObject.GetComponentsInChildren<CardBehaviour>().Length == 1)
        {
            Target();
        }
        else
        {
            DrawNewCard();
            FinishPlaying();
        }

    }

    public override void targetPayoff(EnemyBehaviour enemy)
    {
        DrawNewCard();
        DrawNewCard();
        DrawNewCard();
        DrawNewCard();
        enemy.damage(20);
        player_stats.ExpendAttackMult();
        FinishPlaying();
    }
}
