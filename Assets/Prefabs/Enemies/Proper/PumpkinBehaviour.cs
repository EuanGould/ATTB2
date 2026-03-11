using UnityEngine;

public class PumpkinBehaviour : EnemyBehaviour
{
    public override void damage(int amount)
    {
        attack_damage += 1;
        UpdateAttackDamage();

        base.damage(amount);
    }
}
