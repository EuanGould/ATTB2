using UnityEngine;

public class EruptingSloganBehaviour : CardBehaviour
{
    public override void Play()
    {
        foreach (EnemyBehaviour enemy in GetEnemies())
        {
            DrawNewCard();
            enemy.damage(2);
        }

        FinishPlaying();
    }
}
