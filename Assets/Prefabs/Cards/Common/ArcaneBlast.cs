using UnityEngine;

public class ArcaneBlast : CardBehaviour
{
    public override void Play()
    {
        Target();
        Discard();
    }

    public override void targetPayoff(EnemyBehaviour enemy)
    {
        enemy.damage(5);
    }
}
