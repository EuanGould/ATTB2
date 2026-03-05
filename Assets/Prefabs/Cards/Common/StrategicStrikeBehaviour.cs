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
        FinishPlaying();
    }
}
