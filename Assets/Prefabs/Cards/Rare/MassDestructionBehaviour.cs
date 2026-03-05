using UnityEngine;

public class MassDestructionBehaviour : CardBehaviour
{
    public override void Play()
    {
        foreach (EnemyBehaviour enemy in GetEnemies())
        {
            enemy.damage(30);
        }

        FinishPlaying();
    }
}
