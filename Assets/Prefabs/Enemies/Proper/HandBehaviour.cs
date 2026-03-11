using UnityEngine;

public class HandBehaviour : EnemyBehaviour
{
    public override void Attack()
    {
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamagePlayer(GetComponent<RectTransform>());
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().TakeDamage(attack_damage);
        Heal(attack_damage);
        SetAttackDamage(attack_damage + 2);

    }
}
