using UnityEngine;

public class MagnitudeFiveBehaviour : CardBehaviour
{

    public override void Play()
    {
        Target();
    }

    public override void targetPayoff(EnemyBehaviour enemy)
    {
        foreach (CardBehaviour card in card_selection_manager.gameObject.GetComponentsInChildren<CardBehaviour>())
        {
            if (card != this)
            {
                card.Discard();
                enemy.damage(5);
                player_stats.ExpendAttackMult();
            }
        }
        card_selection_manager.UpdateDeckAndDiscardPileText();

        DrawNewCard();
        DrawNewCard();
        DrawNewCard();
        DrawNewCard();
        DrawNewCard();
        card_selection_manager.DeselectAll();
        FinishPlaying();
    }
}
