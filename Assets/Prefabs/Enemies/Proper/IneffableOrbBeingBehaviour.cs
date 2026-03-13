using UnityEngine;

public class IneffableOrbBeingBehaviour : EnemyBehaviour
{
    private int last_time = 0;

    public override void OnSpawning()
    {
        last_time = time_manager.getTotalTime();
        base.OnSpawning();
    }

    public override void damage(int amount)
    {
        CardBehaviour[] allCards = GameObject.FindGameObjectWithTag("DeckPile").GetComponentsInChildren<CardBehaviour>();

        if (allCards.Length > 0)
        {
            CardBehaviour randomCard = allCards[Random.Range(0, allCards.Length - 1)];

            randomCard.SetCost(-50);
        }



        base.damage(amount);
    }

    public override float onTimeProgressed()
    {
        float delay_to_add = 0.0f;
        int current_time = time_manager.getTotalTime();

        while (current_time >= cooldown_start_time + attack_cooldown)
        {
            InvokeAttack(delay_to_add);
            cooldown_start_time += attack_cooldown;
            delay_to_add += 0.2f;
        }

        countdown_text.text = (attack_cooldown - current_time + cooldown_start_time).ToString();

        if (current_time < last_time)
        {
            GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamagePlayer(GetComponent<RectTransform>());
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().TakeDamage(last_time - current_time);
        }

        last_time = current_time;

        return delay_to_add;
    }
}
