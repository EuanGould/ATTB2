using UnityEngine;

public class IncredibleStrengthBehaviour : CardBehaviour
{
    public override void Play()
    {
        foreach (EnemyBehaviour enemy in GetEnemies())
        {
            enemy.damage(2);
        }

        Discard();
    }
}
