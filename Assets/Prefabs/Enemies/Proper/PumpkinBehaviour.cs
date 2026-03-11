using UnityEngine;

public class PumpkinBehaviour : EnemyBehaviour
{
    public override void damage(int amount)
    {
        CardBehaviour winning_card = GameObject.FindGameObjectWithTag("PlayerHand").GetComponentInChildren<CardBehaviour>();
        
        foreach (CardBehaviour card in GameObject.FindGameObjectWithTag("PlayerHand").GetComponentsInChildren<CardBehaviour>())
        {
            if (winning_card.GetCost() == card.GetCost() && Random.Range(0, 10) % 2 == 0)
            {
                winning_card = card;
            }
            else if (winning_card.GetCost() < card.GetCost())
            {
                winning_card = card;
            }
        }

        winning_card.AddCost(winning_card.GetCost());

        base.damage(amount);
    }
}
